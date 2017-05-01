using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

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
