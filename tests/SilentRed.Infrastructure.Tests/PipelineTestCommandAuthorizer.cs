using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandAuthorizer : ICommandAuthorizer<PipelineTestCommand>
    {
        public Task<CommandResult> AuthorizeAsync(
            PipelineTestCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = new CancellationToken())
        {
            command.Add(typeof(PipelineTestCommandAuthorizer));
            return CommandResult.SucceededTask;
        }
    }
}
