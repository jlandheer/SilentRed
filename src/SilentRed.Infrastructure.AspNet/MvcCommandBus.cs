using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.AspNet
{
    public class MvcCommandBus
    {
        private readonly ICommandBus _commandBus;

        public MvcCommandBus(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task Send<TCommand>(
            TCommand command,
            Headers headers = null,
            CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            await _commandBus.Send(command, headers, cancellationToken);
        }
    }
}

