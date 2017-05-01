using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface ICommandBusinessRule<TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> ValidateAsync(TCommand command, IDictionary<string, object> headers, CancellationToken cancellation = default(CancellationToken));
    }
}
