using MediatR;

namespace Rayonnant.Core.Messaging;

/// <summary>
/// Thin abstraction over MediatR for decoupled module-to-module communication.
/// Modules publish notifications or send requests without knowing the handler.
/// </summary>
public interface IMessageBus
{
    /// <summary>Broadcast a notification to all registered handlers.</summary>
    /// <typeparam name="T">Notification type (implements <see cref="INotification"/>).</typeparam>
    /// <param name="message">The notification payload.</param>
    /// <param name="ct">Cancellation token.</param>
    Task PublishAsync<T>(T message, CancellationToken ct = default) where T : INotification;

    /// <summary>Send a request and return the single handler's response.</summary>
    /// <typeparam name="TResponse">Expected response type.</typeparam>
    /// <param name="request">The request payload.</param>
    /// <param name="ct">Cancellation token.</param>
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}
