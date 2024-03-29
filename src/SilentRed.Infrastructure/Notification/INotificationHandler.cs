﻿using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Notification
{
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
