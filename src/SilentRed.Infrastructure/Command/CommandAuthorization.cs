using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandAuthorization<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task Handle(
            TCommand command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var results = await _authorizers
                .Select(v => v.AuthorizeAsync(command, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                throw new AuthorizationException(command.GetType(), results);
            }

            await _next.Handle(command, headers, cancellationToken);
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
