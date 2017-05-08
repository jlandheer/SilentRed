using System;

namespace SilentRed.Infrastructure.Notification
{
    public class Subscription
    {
        public Guid Id { get; }

        public Subscription(Guid id)
        {
            Id = id;
        }
    }
}
