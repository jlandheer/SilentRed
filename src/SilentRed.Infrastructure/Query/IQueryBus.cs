using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Query
{
    public interface IQueryBus
    {
        Task<TResult> Get<TResult>(
            IQuery<TResult> query,
            Headers headers = null,
            CancellationToken cancellationToken = default);
    }
}
