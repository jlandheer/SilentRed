// ReSharper disable UnusedParameter.Global
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Command
{
    public interface ICommandBusinessRule<in TCommand>
        where TCommand : ICommand
    {
        Task<IEnumerable<Error>> ValidateAsync(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = default);
    }
}
