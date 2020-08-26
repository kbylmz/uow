using ManagementService.Domain.Documents;
using System.Collections.Generic;

namespace ManagementService.Data.Mongo.Collections
{
    public interface IDocumentCollections
    {
        IEnumerable<CustomMongoCollection> All { get; }
    }
    public class DocumentCollections : IDocumentCollections
    {
        public readonly CustomMongoCollection Companies = new CustomMongoCollection("managements", "companies", typeof(CompanyDocument));

        public IEnumerable<CustomMongoCollection> All => new[]
        {
            Companies
        };
    }
}
