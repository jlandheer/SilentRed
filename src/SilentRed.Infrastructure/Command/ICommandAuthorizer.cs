// ReSharper disable UnusedParameter.Global
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandAuthorizer<in TCommand>
        where TCommand : ICommand
    {
        Task<IEnumerable<Error>> AuthorizeAsync(
            TCommand instance,
            Headers headers,
            CancellationToken cancellation = default);
    }
}
