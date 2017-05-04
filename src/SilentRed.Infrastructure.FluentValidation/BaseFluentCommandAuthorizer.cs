// ReSharper disable UnusedMember.Global

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.FluentValidation
{
    public abstract class BaseFluentCommandAuthorizer<TCommand> : AbstractValidator<FluentCommandContext<TCommand>>,
                                                                  ICommandValidator<TCommand>
        where TCommand : ICommand
    {
        Task<IEnumerable<Error>> ICommandValidator<TCommand>.ValidateAsync(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation)
        {
            return ValidateAsync(new FluentCommandContext<TCommand>(command, headers), cancellation).ToErrors();
        }
    }
}
