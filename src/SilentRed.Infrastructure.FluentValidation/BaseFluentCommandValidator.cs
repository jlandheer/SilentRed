// ReSharper disable UnusedMember.Global
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace SilentRed.Infrastructure.FluentValidation
{
    public abstract class BaseFluentCommandValidator<TCommand> : AbstractValidator<FluentCommandContext<TCommand>>, ICommandValidator<TCommand>
        where TCommand : ICommand
    {
        Task<CommandResult> ICommandValidator<TCommand>.ValidateAsync(
            TCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation)
        {
            return ValidateAsync(new FluentCommandContext<TCommand>(command, headers), cancellation).ToCommandResult();
        }
    }
}
