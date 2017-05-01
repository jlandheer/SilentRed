using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface IQueryAuthorizer<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<QueryResult<TResult>> AuthorizeAsync(TQuery query, IDictionary<string, object> headers, CancellationToken cancellation = default(CancellationToken));
    }
}
