using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure
{
    public class CommandBusinessRulesValidation<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var results = await _businessRules
                .Select(v => v.ValidateAsync(command, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                return CommandResult.Failed(nameof(CommandBusinessRulesValidation<TCommand>), results);
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
