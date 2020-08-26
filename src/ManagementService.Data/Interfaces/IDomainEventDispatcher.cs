using ManagementService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementService.Data.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task PublishEvents(List<IEvent> domainEvents);
    }
}
