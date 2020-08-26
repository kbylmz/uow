using System;
using System.Threading.Tasks;

namespace ManagementService.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
