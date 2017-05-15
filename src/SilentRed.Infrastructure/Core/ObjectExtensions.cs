using System.Collections.Generic;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Core
{
    public static class ObjectExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T value)
        {
            return new List<T> { value };
        }

        public static Task<T> AsTask<T>(this T value)
        {
            return Task.FromResult(value);
        }
    }
}
