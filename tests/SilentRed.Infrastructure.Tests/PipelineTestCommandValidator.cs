using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandValidator : ICommandValidator<PipelineTestCommand>
    {
        public Task<CommandResult> ValidateAsync(
            PipelineTestCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = new CancellationToken())
        {
            command.Add(typeof(PipelineTestCommandValidator));
            return CommandResult.SucceededTask;
        }
    }
}
