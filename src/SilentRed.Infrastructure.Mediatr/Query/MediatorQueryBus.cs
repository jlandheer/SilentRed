using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SilentRed.Infrastructure.Core;
using SilentRed.Infrastructure.Query;

namespace SilentRed.Infrastructure.Mediatr
{
    public class MediatorQueryBus : IQueryBus
    {
        public Task<TResult> Get<TResult>(
            IQuery<TResult> query,
            Headers headers,
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
