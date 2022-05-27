using System;
using UniRx;

namespace WelcomeToHell
{
    public static class ScheduleExtensions
    {
        /// <param name="delay">the delay in milliseconds</param>
        public static IDisposable Schedule(this IScheduler scheduler, float delay, Action action)
        {
            return scheduler.Schedule(DateTimeOffset.Now + TimeSpan.FromMilliseconds(delay), action);
        }
    }
}
