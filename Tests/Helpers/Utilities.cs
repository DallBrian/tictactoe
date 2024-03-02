using System.Diagnostics;

namespace Tests.Helpers
{
    public static class Utilities
    {
        public static void WaitFor<T>(this T t, Func<T, bool> condition, string exceptionMessage, long timeoutMs = 2000)
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
