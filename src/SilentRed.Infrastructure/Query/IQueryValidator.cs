using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface IQueryValidator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<QueryResult<TResult>> ValidateAsync(TQuery query, IDictionary<string, object> headers, CancellationToken cancellation = default(CancellationToken));
    }
}
