using System.Reflection;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using Xunit;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTests
    {
        [Fact]
        public async Task DecoratorsAreExecutedInTheRightOrder()
        {
            var assembly = typeof(CommandWithEmptyHandler).GetTypeInfo().Assembly;
            var container = SilentRedTestHelpers.GetContainer(assembly);

            var commandBus = container.GetInstance<ICommandBus>();

            var command = new PipelineTestCommand();
            await commandBus.Send(command);

            var pipeline = command.TypesVisited();

            Assert.Equal(typeof(PipelineTestCommandValidator), pipeline[0]);
            Assert.Equal(typeof(PipelineTestCommandAuthorizer), pipeline[1]);
            Assert.Equal(typeof(PipelineTestCommandBusinessRulesValidator), pipeline[2]);
            Assert.Equal(typeof(PipelineTestCommandHandler), pipeline[3]);
            Assert.Equal(4, pipeline.Count);
        }
    }
}
