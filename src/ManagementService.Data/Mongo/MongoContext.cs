using ManagementService.Data.Interfaces;
using ManagementService.Data.Mongo.Collections;
using ManagementService.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Data.Mongo
{
    public class MongoContext : IDbContext
    {
        private MongoClient mongoClient;
        private IClientSessionHandle session;
        private IDocumentCollections collections;
        private IDomainEventDispatcher eventDispatcher;
        private readonly List<dynamic> trackedEntities;
        private readonly List<Func<Task>> commands;

        public MongoContext(string connectionString, IDocumentCollections documentCollections, IDomainEventDispatcher eventDispatcher)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            commands = new List<Func<Task>>();
            this.eventDispatcher = eventDispatcher;
            trackedEntities = new List<dynamic>();
            mongoClient = new MongoClient(connectionString);
            collections = documentCollections;
        }

        public void Add(dynamic entity)
        {
            Track(entity);
        }
        public async Task<TDocument> GetById<TDocument>(Guid id) where TDocument : class, IAmADocument
        {
            var customMongoCollection = collections.All.Where(p => p.DocumentType == typeof(TDocument)).FirstOrDefault();

            var document = await mongoClient
                .GetDatabase(customMongoCollection.DatabaseName)
                .GetCollection<TDocument>(customMongoCollection.CollectionName).FindAsync(p => p.Id == id);

            return document.FirstOrDefault();
        }
        public async Task<int> SaveChanges()
        {
            using (session = await mongoClient.StartSessionAsync())
            {
                try
                {
                    session.StartTransaction();

                    trackedEntities.ForEach(aggregate => { DecideOperation(aggregate); });

                    var commandTasks = commands.Select(c => c());
                    await Task.WhenAll(commandTasks);

                    await session.CommitTransactionAsync();

                    var domainEvents = trackedEntities.ToList().SelectMany<dynamic, IEvent>(entity => entity.UncommittedEvents).ToList();
                    await eventDispatcher.PublishEvents(domainEvents);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }

            return commands.Count;
        }
        public void Track(dynamic entity)
        {
            trackedEntities.Add(entity);
        }
        
        #region | PRIVATE METHODS |
        private void DecideOperation(dynamic entity)
        {
            var document = entity.ToDocument();

            if (entity.IsPermanentlyDeleted)
            {
                DeleteEntity(document);

                return;
            }

            if (document.Version == 0)
            {
                InsertEntity(document);
            }
            else
            {
                UpdateEntity(document);
            }
        }
        private void InsertEntity(dynamic document)
        {
            var collection = GetCollection(document.GetType());
            document.Version = 1;

            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;

            var wrap = new BsonDocumentWrapper(document);
            commands.Add(async () =>
            {
                await collection.InsertOneAsync(wrap);
            });
        }
        private void UpdateEntity(dynamic document)
        {
            var collection = GetCollection(document.GetType());
            int initialVersion = document.Version;
            document.Version += 1;
            document.UpdatedAt = DateTime.Now;

            var wrap = new BsonDocumentWrapper(document);
            commands.Add(async () =>
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("_id", document.Id) & builder.Eq("Version", initialVersion);

                await collection.ReplaceOneAsync(filter, wrap);
            });
        }
        private void DeleteEntity(dynamic document)
        {
            var collection = GetCollection(document.GetType());

            commands.Add(async () =>
            {
                await collection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", document.Id));
            });
        }
        private IMongoCollection<BsonDocument> GetCollection(Type documentType)
        {
            var customMongoCollection = collections.All.Where(p => p.DocumentType == documentType).FirstOrDefault();

            return mongoClient
                .GetDatabase(customMongoCollection.DatabaseName)
                .GetCollection<BsonDocument>(customMongoCollection.CollectionName);
        }
        #endregion
        public void Dispose()
        {
            //while (session != null && session.IsInTransaction)
            //{
            //    Thread.Sleep(TimeSpan.FromMilliseconds(100));
            //}

            GC.SuppressFinalize(this);
        }
    }
}
