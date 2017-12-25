using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public interface IQueryAuthorizer<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IEnumerable<Error>> AuthorizeAsync(
            TQuery query,
            Headers headers,
            CancellationToken cancellation = default);
    }
}
