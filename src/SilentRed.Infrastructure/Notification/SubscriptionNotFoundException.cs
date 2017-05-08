using System;

namespace SilentRed.Infrastructure.Notification
{
    public class SubscriptionNotFoundException : Exception
    {
        public SubscriptionNotFoundException(Guid subscriptionId)
            : this(subscriptionId, null) { }

        public SubscriptionNotFoundException(Guid subscriptionId, Exception inner)
            : base($"Subscription with Id {subscriptionId} not found.", inner) { }
    }
}
