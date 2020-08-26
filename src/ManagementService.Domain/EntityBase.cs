using System;
using System.Collections.Generic;

namespace ManagementService.Domain
{

    public abstract class EntityBase<TDocument> : IEntityBase<TDocument> where TDocument : IAmADocument, new()
    {
        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public bool IsPermanentlyDeleted { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IList<IEvent> UncommittedEvents { get; private set; }

        protected EntityBase()
        {
            UncommittedEvents = new List<IEvent>();
        }

        protected EntityBase(Guid id)
        {
            Id = id;
            UncommittedEvents = new List<IEvent>();
        }

        public virtual void DeletePermanently()
        {
            IsPermanentlyDeleted = true;
        }
        protected void RaiseEvent(IEvent anEvent)
        {
            UncommittedEvents.Add(anEvent);
        }
        public void ClearEvents()
        {
            UncommittedEvents = new List<IEvent>();
        }
        public abstract void Load(TDocument document);

        public abstract TDocument ToDocument();
    }
}
