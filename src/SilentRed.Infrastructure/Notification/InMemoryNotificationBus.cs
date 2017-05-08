using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Notification
{
    public class InMemoryNotificationBus : INotificationBus, IDisposable
    {
        public void Dispose()
        {
            _dispatcher?.Dispose();
            _registrations?.Clear();
        }

        public Task Publish<TNotification>(
            TNotification notification,
            CancellationToken cancellationToken = new CancellationToken())
            where TNotification : INotification
        {
            _dispatcher.Dispatch(_registrations.GetHandlersFor(notification));

            _publishedNotifications.Add(notification);
            return Task.CompletedTask;
        }

        public IList<INotification> PublishedNotifications()
        {
            return _publishedNotifications.ToList();
        }

        public Task<Subscription> Subscribe<TNotification>(
            Action<TNotification> action,
            CancellationToken cancellationToken = new CancellationToken())
            where TNotification : INotification
        {
            var subscriptionId = _registrations.Add(action);

            return Task.FromResult(new Subscription(subscriptionId));
        }


        public Task UnSubscribe(
            Subscription subscription,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _registrations.UnSubscribe(subscription.Id);

            return Task.CompletedTask;
        }

        public InMemoryNotificationBus()
        {
            _dispatcher = new NotificationDispatcher();
            _publishedNotifications = new AppendOnlyList<INotification>();
            _registrations = new NotificationRegistrations();
            _dispatcher.Start();
        }

        private readonly AppendOnlyList<INotification> _publishedNotifications;
        private readonly NotificationRegistrations _registrations;
        private readonly NotificationDispatcher _dispatcher;
    }
}
