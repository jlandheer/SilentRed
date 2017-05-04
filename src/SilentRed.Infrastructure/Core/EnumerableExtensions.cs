using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Core
{
    public static class EnumerableExtensions
    {
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks.ToList());
        }
    }
}
