using MediatR;
using Microsoft.Extensions.Logging;
using SeaPizza.Application.Common.Events;
using SeaPizza.Shared.Events;
using System.Threading.Tasks;
using System;

namespace SeaPizza.Infrastructure.Common.Services;

public class EventPublisher : IEventPublisher
{
    public Task PublishAsync(IEvent @event)
    {
        throw new NotImplementedException();
    }
}
