using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.AspNet
{
    public class MvcQueryBus
    {
        private readonly IQueryBus _queryBus;

        public MvcQueryBus(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        public Task<TResult> Get<TResult>(
            IQuery<TResult> query,
            Headers headers = null,
            CancellationToken cancellationToken = default)
        {
            return _queryBus.Get(query, headers, cancellationToken);
        }
    }
}

