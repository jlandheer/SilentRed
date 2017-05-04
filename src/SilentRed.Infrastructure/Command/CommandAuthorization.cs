using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure
{
    public class CommandAuthorization<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var results = await _authorizers
                .Select(v => v.AuthorizeAsync(command, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                return CommandResult.Failed(nameof(CommandAuthorization<TCommand>), results);
            }

            return await _next.Handle(command, headers, cancellationToken);
        }

        public CommandAuthorization(
            ICommandHandler<TCommand> next,
            ICommandAuthorizer<TCommand>[] authorizers)
        {
            _next = next;
            _authorizers = authorizers;
        }

        private readonly ICommandHandler<TCommand> _next;
        private readonly ICommandAuthorizer<TCommand>[] _authorizers;
    }
}
