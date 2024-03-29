using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Tests
{
    public class CommandWithValidator : ICommand { }

    public class CommandWithValidatorHandler : ICommandHandler<CommandWithValidator>
    {
        public Task Handle(
            CommandWithValidator command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class CommandWithValidatorFailingValidator : ICommandValidator<CommandWithValidator>
    {
        public Task<IEnumerable<Error>> ValidateAsync(
            CommandWithValidator command,
            Headers headers,
            CancellationToken cancellation = new CancellationToken())
        {
            return new Error("Foutje, bedankt.").AsEnumerable().AsTask();
        }
    }
}
