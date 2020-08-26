using ManagementService.Domain.Documents;
using ManagementService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Data.Interfaces
{
    public interface ICompanyRepository : IRepository<Company, CompanyDocument>
    {
    }
}
