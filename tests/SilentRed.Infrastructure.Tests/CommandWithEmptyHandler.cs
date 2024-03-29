using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

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
