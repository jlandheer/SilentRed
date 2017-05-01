using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SilentRed.Infrastructure.Mediatr
{
    public class MediatorQueryBus : IQueryBus
    {
        public Task<QueryResult<TResult>> Get<TResult>(
            IQuery<TResult> query,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return _mediator.Send(MediatorQueryWrapper.Wrap(query, headers), cancellationToken);
        }

        public MediatorQueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;
    }
}
