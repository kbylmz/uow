using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Domain.Documents
{
    public class CompanyDocument: IAmADocument
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }     
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
