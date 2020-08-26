using System;

namespace ManagementService.Data.Mongo.Collections
{
    public class CustomMongoCollection
    {
        public string DatabaseName { get; }
        public string CollectionName { get; }
        public Type DocumentType { get;}

        public CustomMongoCollection(string databaseName, string collectionName, Type documentType)
        {
            DatabaseName = databaseName;
            CollectionName = collectionName;
            DocumentType = documentType;
        }
    }
}
