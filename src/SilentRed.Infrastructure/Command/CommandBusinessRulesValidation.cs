using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandBusinessRulesValidation<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task Handle(
            TCommand command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            var results = await _businessRules
                .Select(v => v.ValidateAsync(command, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                throw new BusinessRuleException(command.GetType(), results);
            }

            await _next.Handle(command, headers, cancellationToken);
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
