using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public interface IQueryValidator<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IEnumerable<Error>> ValidateAsync(
            TQuery query,
            Headers headers,
            CancellationToken cancellation = default);
    }
}
