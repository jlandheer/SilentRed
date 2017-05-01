using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public static class ObjectExtensions
    {
        public static Task<T> AsTask<T>(this T value)
        {
            return Task.FromResult(value);
        }
    }
}