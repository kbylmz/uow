using System;

namespace ManagementService.Domain
{
    public interface IAmADocument
    {
        Guid Id { get; set; }

        int Version { get; set; }
    }
}