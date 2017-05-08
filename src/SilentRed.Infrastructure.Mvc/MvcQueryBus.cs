using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.Mvc
{
    public class MvcQueryBus
    {
        private readonly IQueryBus _queryBus;

        public MvcQueryBus(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        public async Task<TResult> Get<TResult>(
            IQuery<TResult> query,
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await _queryBus.Get(query, headers, cancellationToken)).ValueOrThrow();
        }
    }
}

