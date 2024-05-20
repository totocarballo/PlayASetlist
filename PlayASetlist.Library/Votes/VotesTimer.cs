using System;
using System.Threading;

namespace PlayASetlist.Library.Votes
{
    public class VotesTimer : IDisposable
    {
        private readonly Timer timer;
        private readonly Action timerCallback;
        private bool isRunning;

        public VotesTimer(Action callback, int intervalMilliseconds)
        {
            timerCallback = callback;
            timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
            IntervalMilliseconds = intervalMilliseconds;
        }

        public int IntervalMilliseconds { get; }

        public void Dispose()
        {
            timer.Dispose();
        }

        public void Pause()
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Resume()
        {
            timer.Change(0, IntervalMilliseconds);
        }

        public void Start()
        {
            lock (timer)
            {
                if (!isRunning)
                {
                    timer.Change(0, IntervalMilliseconds);
                    isRunning = true;
                }
            }
        }

        public void Stop()
        {
            lock (timer)
            {
                if (isRunning)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    isRunning = false;
                }
            }
        }

        private void TimerCallback(object state)
        {
            lock (timer)
            {
                if (!isRunning)
                {
                    return;
                }

                timerCallback?.Invoke();
            }
        }
    }
}