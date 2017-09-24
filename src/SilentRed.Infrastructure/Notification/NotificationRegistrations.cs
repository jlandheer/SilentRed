using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SilentRed.Infrastructure.Notification
{
    public class NotificationRegistrations
    {
        public Guid Add<TNotification>(Action<TNotification> handler)
            where TNotification : INotification
        {
            var notificationType = typeof(TNotification);
            var subscriptions =
                (SubscriptionList<TNotification>)
                _registrations.GetOrAdd(notificationType, new SubscriptionList<TNotification>());
            return subscriptions.AddSubscription(handler);
        }

        public void Clear()
        {
            _registrations.Clear();
        }

        public IEnumerable<Action> GetHandlersFor<TNotification>(TNotification notification)
            where TNotification : INotification
        {
            return GetSubscriptionsFor<TNotification>()?.GetHandlersFor(notification);
        }

        public void UnSubscribe(Guid subscriptionId)
        {
            var success = false;
            foreach (var subscriptionList in _registrations.Values)
            {
                success = success || subscriptionList.UnSubscribe(subscriptionId);
            }

            if (!success)
            {
                throw new SubscriptionNotFoundException(subscriptionId);
            }
        }

        private SubscriptionList<TNotification> GetSubscriptionsFor<TNotification>()
            where TNotification : INotification
        {
            var type = typeof(TNotification);
            var subscriptionList = _registrations.GetOrAdd(type, new SubscriptionList<TNotification>());

            return (SubscriptionList<TNotification>) subscriptionList;
        }

        private readonly ConcurrentDictionary<Type, SubscriptionList>
            _registrations = new ConcurrentDictionary<Type, SubscriptionList>();
    }
}
