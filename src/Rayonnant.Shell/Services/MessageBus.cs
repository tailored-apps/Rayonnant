using MediatR;
using Rayonnant.Core.Messaging;

namespace Rayonnant.Shell.Services;

public class MessageBus(IMediator mediator) : IMessageBus
{
    public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : INotification
        => mediator.Publish(message, ct);

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
        => mediator.Send(request, ct);
}
