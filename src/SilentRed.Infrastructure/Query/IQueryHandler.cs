using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<QueryResult<TResult>> Handle(TQuery query, IDictionary<string, object> headers, CancellationToken cancellationToken);
    }
}
