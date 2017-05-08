using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public class CommandValidation<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            var results = await _validators
                .Select(v => v.ValidateAsync(command, headers, cancellationToken))
                .WhenAll()
                .Flatten();

            if (results.Any())
            {
                return new CommandValidationFailed(command, results);
            }

            return await _next.Handle(command, headers, cancellationToken);
        }

        public CommandValidation(
            ICommandHandler<TCommand> next,
            ICommandValidator<TCommand>[] validators)
        {
            _next = next;
            _validators = validators;
        }

        private readonly ICommandHandler<TCommand> _next;
        private readonly ICommandValidator<TCommand>[] _validators;
    }
}
