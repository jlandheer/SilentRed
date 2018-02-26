using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Query
{
    public class QueryAuthorization<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        public async Task<TResult> Handle(
            TQuery query,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var results = await _authorizers
                .Select(v => v.AuthorizeAsync(query, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                throw new AuthorizationException(query.GetType(), results);
            }

            return await _next.Handle(query, headers, cancellationToken);
        }

        public QueryAuthorization(
            IQueryHandler<TQuery, TResult> next,
            IQueryAuthorizer<TQuery, TResult>[] authorizers)
        {
            _next = next;
            _authorizers = authorizers;
        }

        private readonly IQueryHandler<TQuery, TResult> _next;
        private readonly IQueryAuthorizer<TQuery, TResult>[] _authorizers;
    }
}
