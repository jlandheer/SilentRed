using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> Handle(TCommand command, IDictionary<string, object> headers, CancellationToken cancellationToken);
    }
}
