using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Mediatr
{
    public class CommandHandlerWrappedForMediator<TCommand> :
        ICancellableAsyncRequestHandler<CommandWrappedForMediator<TCommand>>
        where TCommand : ICommand
    {
        public Task Handle(
            CommandWrappedForMediator<TCommand> message,
            CancellationToken cancellationToken)
        {
            return _commandHandler.Handle(message.Command, message.Headers, cancellationToken);
        }

        public CommandHandlerWrappedForMediator(ICommandHandler<TCommand> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        private readonly ICommandHandler<TCommand> _commandHandler;
    }
}
