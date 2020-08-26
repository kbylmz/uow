using ManagementService.Data.Interfaces;
using ManagementService.Domain;
using System;
using System.Threading.Tasks;

namespace ManagementService.Data
{
    public abstract class BaseRepository<TEntity, TDocument> : IRepository<TEntity, TDocument>
        where TEntity : IEntityBase<TDocument>, new()
        where TDocument : class, IAmADocument
    {
        protected readonly IDbContext _context;

        protected BaseRepository(IDbContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public virtual async Task<Optional<TEntity>> GetById(Guid id) 
        {
            var document = await _context.GetById<TDocument>(id);

            if (document == null)
            {
                return Optional<TEntity>.Empty;
            }

            var entity = new TEntity();
            entity.Load(document);
            _context.Track(entity);
            
            return new Optional<TEntity>(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
