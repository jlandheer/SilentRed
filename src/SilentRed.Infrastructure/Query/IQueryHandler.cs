using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Query
{
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(
            TQuery query,
            Headers headers,
            CancellationToken cancellationToken);
    }
}
