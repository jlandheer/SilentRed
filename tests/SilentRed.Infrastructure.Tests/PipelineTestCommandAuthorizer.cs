using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandAuthorizer : ICommandAuthorizer<PipelineTestCommand>
    {
        public Task<IEnumerable<Error>> AuthorizeAsync(
            PipelineTestCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellation = new CancellationToken())
        {
            command.Add(typeof(PipelineTestCommandAuthorizer));

            return Error.NoErrors.AsTask();
        }
    }
}
