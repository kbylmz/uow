using ManagementService.Data.Interfaces;
using ManagementService.Domain.Documents;
using ManagementService.Domain.Models;

namespace ManagementService.Data.Mongo
{
    public class CompanyRepository : BaseRepository<Company, CompanyDocument>, ICompanyRepository
    {
        public CompanyRepository(IDbContext context) : base(context)
        {
        }
    }
}
