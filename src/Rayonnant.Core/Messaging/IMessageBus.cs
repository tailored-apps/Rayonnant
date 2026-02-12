using MediatR;

namespace Rayonnant.Core.Messaging;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken ct = default) where T : INotification;
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
