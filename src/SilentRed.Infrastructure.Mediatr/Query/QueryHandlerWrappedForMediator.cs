using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SilentRed.Infrastructure.Mediatr
{
    public class QueryHandlerWrappedForMediator<TQuery, TQueryResult>
        : ICancellableAsyncRequestHandler<QueryWrappedForMediator<TQuery, TQueryResult>, QueryResult<TQueryResult>>
        where TQuery : IQuery<TQueryResult>
    {
        public Task<QueryResult<TQueryResult>> Handle(
            QueryWrappedForMediator<TQuery, TQueryResult> wrappedQuery,
            CancellationToken cancellationToken)
        {
            return _internalHandler.Handle(wrappedQuery.Query, wrappedQuery.Headers, cancellationToken);
        }

        public QueryHandlerWrappedForMediator(IQueryHandler<TQuery, TQueryResult> internalHandler)
        {
            _internalHandler = internalHandler;
        }

        private readonly IQueryHandler<TQuery, TQueryResult> _internalHandler;
    }
}
