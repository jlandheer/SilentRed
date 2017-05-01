using System;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Tests
{
    public static class Act
    {
        public static Exception Try(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static async Task<Exception> TryAsync(Func<Task> action)
        {
            try
            {
                await action();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}