using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Mvc
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
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand
        {
            (await _commandBus.Send(command, headers, cancellationToken)).ThrowIfNeeded();
        }
    }
}

