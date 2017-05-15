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
        public Task<CommandResult> Handle(
            CommandWithValidator command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            return CommandSuccess.NewTask();
        }
    }

    public class CommandWithValidatorFailingValidator : ICommandValidator<CommandWithValidator>
    {
        public Task<IEnumerable<Error>> ValidateAsync(
            CommandWithValidator command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = new CancellationToken())
        {
            return new Error("Foutje, bedankt.").AsEnumerable().AsTask();
        }
    }
}
