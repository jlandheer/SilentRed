using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface ICommandBus
    {
        Task<CommandResult> Send<TCommand>(
            TCommand command,
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default(CancellationToken)
        )
            where TCommand : ICommand;
    }
}
