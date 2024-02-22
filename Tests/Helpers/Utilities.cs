using System.Diagnostics;

namespace Tests.Helpers
{
    public static class Utilities
    {
        public static void WaitFor(Func<bool> condition, string exceptionMessage, long timeoutMs = 100)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < timeoutMs)
            {
                if (condition.Invoke())
                {
                    return;
                }
            }

            throw new(exceptionMessage);
        }

        public static void WaitFor<T>(this T t, Func<T, bool> condition, string exceptionMessage, long timeoutMs = 100)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < timeoutMs)
            {
                if (condition.Invoke(t))
                {
                    return;
                }
            }

            throw new(exceptionMessage);
        }
    }
}
