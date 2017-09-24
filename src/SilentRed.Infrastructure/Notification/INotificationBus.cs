// ReSharper disable UnusedParameter.Global
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Notification
{
    public interface INotificationBus
    {
        Task Publish<TNotification>(
            TNotification notification,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;

        Task<Subscription> Subscribe<TNotification>(
            Action<TNotification> action,
            CancellationToken cancellationToken = default)
            where TNotification : INotification;

        Task UnSubscribe(
            Subscription subscription,
            CancellationToken cancellationToken = default);
    }
}
