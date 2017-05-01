using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Tests
{
    public class CommandWithEmptyHandler : ICommand { }

    public class CommandWithEmptyHandlerHandler : ICommandHandler<CommandWithEmptyHandler>
    {
        public Task<CommandResult> Handle(CommandWithEmptyHandler command, IDictionary<string, object> headers, CancellationToken cancellationToken)
        {
            return CommandResult.SucceededTask;
        }
    }
}
