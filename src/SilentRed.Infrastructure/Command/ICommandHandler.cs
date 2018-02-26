using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        Task Handle(
            TCommand command,
            Headers headers,
            CancellationToken cancellationToken);
    }
}
