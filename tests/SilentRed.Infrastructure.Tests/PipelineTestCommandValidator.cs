using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandValidator : ICommandValidator<PipelineTestCommand>
    {
        public Task<IEnumerable<Error>> ValidateAsync(
            PipelineTestCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = new CancellationToken())
        {
            command.Add(typeof(PipelineTestCommandValidator));

            return Error.NoErrors.AsTask();
        }
    }
}
