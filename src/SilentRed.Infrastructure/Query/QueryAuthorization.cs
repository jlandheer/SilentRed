using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public class QueryAuthorization<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public Task<QueryResult<TResult>> Handle(TQuery query, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            return _next.Handle(query, headers, cancellationToken);
        }

        public QueryAuthorization(IQueryHandler<TQuery, TResult> next, IQueryAuthorizer<TQuery, TResult>[] authorizers)
        {
            _next = next;
            _authorizers = authorizers;
        }

        private readonly IQueryHandler<TQuery, TResult> _next;
        private readonly IQueryAuthorizer<TQuery, TResult>[] _authorizers;
    }
}
