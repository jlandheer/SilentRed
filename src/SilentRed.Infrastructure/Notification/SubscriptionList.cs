using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SilentRed.Infrastructure.Notification
{
    public class SubscriptionList<TNotification> : SubscriptionList
        where TNotification : INotification
    {
        public Guid AddSubscription(Action<TNotification> handler)
        {
            var id = Guid.NewGuid();
            _subscriptions.TryAdd(id, handler);
            return id;
        }

        public IEnumerable<Action> GetHandlersFor(TNotification @notification)
        {
            return _subscriptions.Values.Select(action => (Action) (() => action(@notification)));
        }

        public override bool UnSubscribe(Guid id)
        {
            Action<TNotification> dummy;
            return _subscriptions.TryRemove(id, out dummy);
        }

        private readonly ConcurrentDictionary<Guid, Action<TNotification>> _subscriptions =
            new ConcurrentDictionary<Guid, Action<TNotification>>();
    }

    public abstract class SubscriptionList
    {
        public abstract bool UnSubscribe(Guid subscriptionId);
    }
}
