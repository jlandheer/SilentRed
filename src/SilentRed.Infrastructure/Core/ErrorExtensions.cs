// ReSharper disable MemberCanBePrivate.Global

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Core
{
    public static class ErrorExtensions
    {
        public static async Task<IList<Error>> Flatten(this Task<IEnumerable<Error>[]> errors)
            => (await errors)
                .SelectMany(i => i)
                .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                .Where(i => i.Any())
                .Select(i => new Error(i.SelectMany(m => m.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                .ToList();

        public static IList<Error> Flatten(this IEnumerable<Error> errors)
            => errors
                .GroupBy(i => new { i.PropertyName, i.AttemptedValue })
                .Where(i => i.Any())
                .Select(i => new Error(i.SelectMany(m => m.Messages), i.Key.PropertyName, i.Key.AttemptedValue))
                .ToList();
    }
}
