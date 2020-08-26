using ManagementService.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementService.Domain.Events
{

    public class CompanyCreatedEvent : IEvent
    {
        public Guid Id { get; }
        public Guid AggregateId { get; }
        public string Payload { get; }

        public CompanyCreatedEvent(Guid aggregateId, string payload)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
            Payload = payload;
        }

        public object Clone()
        {
            return this;
        }
    }
}
