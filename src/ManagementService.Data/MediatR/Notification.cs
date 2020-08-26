using ManagementService.Domain;
using MediatR;

namespace ManagementService.Data.MediatR
{
    public class Notification : INotification
    {
        public IEvent Event { get; }

        public Notification(IEvent notification)
        {
            Event = notification;
        }
    }
}
