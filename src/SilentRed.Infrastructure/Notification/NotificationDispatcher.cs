using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace SilentRed.Infrastructure.Notification
{
    public class NotificationDispatcher : IDisposable
    {
        public void Cancel()
        {
            if (_started)
            {
                _tokenSource.Cancel();
            }
        }

        public void Dispatch(Action action)
        {
            Start();
            _dispatchQueue.Add(action);
        }

        public void Dispatch(IEnumerable<Action> actions)
        {
            foreach (var action in actions)
            {
                Dispatch(action);
            }
        }

        public void Dispose()
        {
            if (_started)
            {
                _tokenSource.Cancel();
            }
        }

        public void Start()
        {
            if (_started)
            {
                return;
            }

            _started = true;
            _tokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(state => StartLoop(_tokenSource.Token));
        }


        public int DispatchedCount { get; private set; }

        public int QueueLength => _dispatchQueue.Count;

#if DEBUG

        public NotificationDispatcher(int delay)
        {
            _delay = delay;
            _dispatchQueue = new BlockingCollection<Action>();
            _started = false;
            DispatchedCount = 0;
            _tokenSource = new CancellationTokenSource();
        }

#endif

        public NotificationDispatcher()
        {
            _dispatchQueue = new BlockingCollection<Action>();
            _started = false;
            DispatchedCount = 0;
            _tokenSource = new CancellationTokenSource();
        }

#if DEBUG
        private readonly int _delay;
#endif
        private readonly BlockingCollection<Action> _dispatchQueue;
        private bool _started;
        private CancellationTokenSource _tokenSource;

        private void StartLoop(CancellationToken token)
        {
            while (true)
            {
                try
                {
                    var haveOne = _dispatchQueue.TryTake(out var action, 100, token);
                    if (haveOne)
                    {
                        action();
                        DispatchedCount++;
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
#if DEBUG
                if (_delay > 0)
                {
                    Thread.Sleep(_delay);
                }
#endif
            }
        }
    }
}
