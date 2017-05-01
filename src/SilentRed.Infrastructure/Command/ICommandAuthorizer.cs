using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure
{
    public interface ICommandAuthorizer<in TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> AuthorizeAsync(TCommand instance, IDictionary<string, object> headers, CancellationToken cancellation = default(CancellationToken));
    }
}
