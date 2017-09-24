using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken);
    }
}
