using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Mediatr
{
    public class CommandHandlerWrappedForMediator<TCommand, TResult> :
        ICancellableAsyncRequestHandler<CommandWrappedForMediator<TCommand, TResult>, CommandResult>
        where TCommand : ICommand
        where TResult : CommandResult
    {
        public Task<CommandResult> Handle(
            CommandWrappedForMediator<TCommand, TResult> message,
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
