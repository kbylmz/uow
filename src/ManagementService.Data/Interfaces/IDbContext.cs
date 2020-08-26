using ManagementService.Domain;
using System;
using System.Threading.Tasks;

namespace ManagementService.Data.Interfaces
{
    public interface IDbContext
    {
        void Add(dynamic entity);
        Task<TDocument> GetById<TDocument>(Guid id) where TDocument : class, IAmADocument;
        Task<int> SaveChanges();    
        void Track(dynamic entity);
        void Dispose();    
    }
}
