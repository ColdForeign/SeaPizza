using SeaPizza.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace SeaPizza.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}
