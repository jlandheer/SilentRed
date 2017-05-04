using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface IQueryBus
    {
        Task<QueryResult<TResult>> Get<TResult>(
            IQuery<TResult> query,
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
