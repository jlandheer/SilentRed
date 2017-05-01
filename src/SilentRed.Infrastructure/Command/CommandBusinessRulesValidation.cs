using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public class CommandBusinessRulesValidation<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(TCommand command, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            var tasks = _businessRules
                .Select(v => v.ValidateAsync(command, headers, cancellationToken));

            var results = await Task.WhenAll(tasks);
            var failures = results.SelectMany(result => result.Errors)
                                  .Where(f => f != null)
                                  .ToList();

            if (failures.Any())
            {
                return CommandResult.Failed(failures);
            }

            return await _next.Handle(command, headers, cancellationToken);
        }

        public CommandBusinessRulesValidation(
            ICommandHandler<TCommand> next,
            ICommandBusinessRule<TCommand>[] businessRules)
        {
            _next = next;
            _businessRules = businessRules;
        }

        private readonly ICommandHandler<TCommand> _next;
        private readonly ICommandBusinessRule<TCommand>[] _businessRules;
    }
}
