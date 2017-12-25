using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Tests
{
    public class CommandWithEmptyHandler : ICommand { }

    public class CommandWithEmptyHandlerHandler : ICommandHandler<CommandWithEmptyHandler>
    {
        public Task Handle(
            CommandWithEmptyHandler command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
