using ManagementService.Data.Interfaces;
using ManagementService.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Data.MediatR
{
    public class MediatrDomainEventDispatcher: IDomainEventDispatcher
    {
        private readonly IMediator dispatcher;
        public MediatrDomainEventDispatcher(IMediator dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public async Task PublishEvents(List<IEvent> domainEvents)
        {
            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                var notification = new Notification(domainEvent);
                await dispatcher.Publish(notification);
            });

            await Task.WhenAll(tasks);
        }
    }
}
