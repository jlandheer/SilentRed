using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure
{
    public interface IQueryAuthorizer<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<IEnumerable<Error>> AuthorizeAsync(
            TQuery query,
            IDictionary<string, object> headers,
            CancellationToken cancellation = default(CancellationToken));
    }
}
