using ManagementService.Domain.Documents;
using ManagementService.Domain.Events;
using System;

namespace ManagementService.Domain.Models
{
    public class Company: EntityBase<CompanyDocument>
    {
        public string Name { get; private set; }

        public Company()
        {

        }

        public Company(Guid id, string name) 
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.Now;

            RaiseEvent(new CompanyCreatedEvent(id, name));
        }

        public override void Load(CompanyDocument document)
        {
            Id = document.Id;
            Version = document.Version;
            Name = document.Name;
            CreatedAt = document.CreatedAt;
            UpdatedAt = document.UpdatedAt;
        }

        public override CompanyDocument ToDocument()
        {
            var companyDocument = new CompanyDocument
            {
                Id = Id,
                Name = Name,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
            
            return companyDocument;
        }
    }
}
