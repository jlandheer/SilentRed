using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandBus
    {
        Task Send<TCommand>(
            TCommand command,
            IDictionary<string, object> headers = null,
            CancellationToken cancellationToken = default
        )
            where TCommand : ICommand;
    }
}
