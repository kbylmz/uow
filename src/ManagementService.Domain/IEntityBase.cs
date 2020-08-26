using System;
using System.Collections.Generic;

namespace ManagementService.Domain
{
    public interface IEntityBase<TDocument>
    {
        Guid Id { get; }

        int Version { get; }

        IList<IEvent> UncommittedEvents { get; }

        bool IsPermanentlyDeleted { get; }

        void DeletePermanently();

        void Load(TDocument document);

        TDocument ToDocument();
    }
}