using ManagementService.Data.Interfaces;
using ManagementService.Domain;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementService.Data.MassTransit
{
    public class MassTransitDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IBusControl dispatcher;
        public MassTransitDomainEventDispatcher(IBusControl dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        public async Task PublishEvents(List<IEvent> domainEvents)
        {
            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await dispatcher.Publish(domainEvent.Clone());
            });

            await Task.WhenAll(tasks);
        }
    }
}
