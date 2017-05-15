using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandHandler : ICommandHandler<PipelineTestCommand>
    {
        public Task<CommandResult> Handle(
            PipelineTestCommand command,
            IDictionary<string, object> headers,
            CancellationToken cancellationToken)
        {
            command.Add(typeof(PipelineTestCommandHandler));

            return CommandSuccess.NewTask();
        }
    }
}
