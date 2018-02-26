using System.Threading;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommandHandler : ICommandHandler<PipelineTestCommand>
    {
        public Task Handle(
            PipelineTestCommand command,
            Headers headers,
            CancellationToken cancellationToken)
        {
            command.Add(typeof(PipelineTestCommandHandler));

            return Task.CompletedTask;
        }
    }
}
