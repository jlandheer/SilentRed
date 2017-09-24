using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Query
{
    public interface IQueryBus
    {
        Task<TResult> Get<TResult>(
            IQuery<TResult> query,
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
