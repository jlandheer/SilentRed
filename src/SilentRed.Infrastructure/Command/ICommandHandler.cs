using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> Handle(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken);
    }
}
