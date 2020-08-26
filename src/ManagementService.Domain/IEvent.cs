using System;

namespace ManagementService.Domain
{
    public interface IEvent: ICloneable
    {
        Guid Id { get; }
        Guid AggregateId { get; }
        string Payload { get; }
    }
}
