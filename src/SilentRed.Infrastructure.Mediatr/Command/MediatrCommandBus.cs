using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Mediatr
{
    public class MediatorCommandBus : ICommandBus
    {
        public Task Send<TCommand>(
            TCommand command,
            Headers headers = null,
            CancellationToken cancellationToken = new CancellationToken())
            where TCommand : ICommand
        {
            if(command==null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            try
            {
                return _mediator.Send(
                    new CommandWrappedForMediator<TCommand>(command, headers),
                    cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new NoCommandHandlerFoundException(command.GetType(), ex);
            }
        }

        public MediatorCommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;
    }
}
