using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandValidator<in TCommand>
        where TCommand : ICommand
    {
        Task<IEnumerable<Error>> ValidateAsync(
            TCommand command,
            Headers headers,
            CancellationToken cancellation = default);
    }
}
