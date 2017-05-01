using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SilentRed.Infrastructure
{
    public class CommandAuthorization<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(TCommand command, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            var tasks = _authorizers
                .Select(v => v.AuthorizeAsync(command, headers, cancellationToken));

            var results = await Task.WhenAll(tasks);
            var failures = results.SelectMany(result => result.Errors)
                                  .Where(f => f != null)
                                  .ToList();

            if (failures.Any())
            {
                return
                    CommandResult.Failed(failures);
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
