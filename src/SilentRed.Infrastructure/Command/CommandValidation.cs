using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public class CommandValidation<TCommand> : ICommandDecorator<TCommand>
        where TCommand : ICommand
    {
        public async Task<CommandResult> Handle(TCommand command, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            var tasks = _validators
                .Select(v => v.ValidateAsync(command, headers, cancellationToken));

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
