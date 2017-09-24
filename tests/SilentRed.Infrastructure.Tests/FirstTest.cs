using System;
using System.Reflection;
using System.Threading.Tasks;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Core;
using Xunit;

namespace SilentRed.Infrastructure.Tests
{
    public class FirstTest
    {
        [Fact]
        public async Task GivenACommandWithAFailingValidatorAFailedResultIsReturned()
        {
            var assembly = typeof(CommandWithEmptyHandler).GetTypeInfo().Assembly;
            var container = SilentRedTestHelpers.GetContainer(assembly);

            var commandBus = container.GetInstance<ICommandBus>();

            var ex = await Act.TryAsync(() => commandBus.Send(new CommandWithValidator()));
            Assert.NotNull(ex);
        }

        [Fact]
        public async Task GivenACommandWithASuccesHandlerASuccessResultIsReturned()
        {
            var assembly = typeof(CommandWithEmptyHandler).GetTypeInfo().Assembly;
            var container = SilentRedTestHelpers.GetContainer(assembly);

            var commandBus = container.GetInstance<ICommandBus>();

            var ex = await Act.TryAsync(() => commandBus.Send(new CommandWithEmptyHandler()));
            Assert.NotNull(ex);
        }

        [Fact]
        public async Task GivenACommandWithoutAnHandlerTheCorrectExceptionIsThrown()
        {
            var assembly = typeof(CommandWithEmptyHandler).GetTypeInfo().Assembly;
            var container = SilentRedTestHelpers.GetContainer(assembly);

            var commandBus = container.GetInstance<ICommandBus>();

            var ex = await Act.TryAsync(() => commandBus.Send(new CommandWithoutHandler()));

            Assert.IsType<NoCommandHandlerFoundException>(ex);
        }

        [Fact]
        public async Task GivenNullAsACommandAnNullReferenceExceIsThrown()
        {
            var assembly = typeof(CommandWithEmptyHandler).GetTypeInfo().Assembly;
            var container = SilentRedTestHelpers.GetContainer(assembly);

            var commandBus = container.GetInstance<ICommandBus>();

            var ex = await Act.TryAsync(() => commandBus.Send((CommandWithValidator)null));

            Assert.IsType<ArgumentNullException>(ex);
        }
    }
}
