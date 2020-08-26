using ManagementService.Domain;
using System;
using System.Threading.Tasks;

namespace ManagementService.Data.Interfaces
{
    public interface IRepository<TEntity, TDocument> : IDisposable 
        where TEntity : IEntityBase<TDocument>
        where TDocument : IAmADocument
    {
        void Add(TEntity entity);
        Task<Optional<TEntity>> GetById(Guid id);
    }
}
