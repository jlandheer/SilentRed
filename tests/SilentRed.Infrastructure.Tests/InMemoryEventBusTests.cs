//using System.Threading.Tasks;
//using Utilities.xUnit;
//using Xunit;

//namespace SilentRed.Infrastructure.Tests
//{
//    public class InMemoryEventBusTests
//    {
//        [Fact]
//        public async Task CanUnSubscribe()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler = new TestHandler(0);
//            var id = await bus.Subscribe<TestEvent1>(i => handler.Handle(i));
//            var ex = Act.TryAsync(() => bus.UnSubscribe(id));

//            await handler.AllDone();
//            Assert.Null(ex);
//        }

//        [Fact]
//        public async Task EventIsPassedToHandler()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler = new TestHandler(1);
//            await bus.Subscribe<TestEvent1>(i => handler.Handle(i));

//            // act
//            await bus.Publish(new TestEvent1());

//            await handler.AllDone();

//            // assert
//            Assert.Equal(1, handler.Invocations);
//        }

//        [Fact]
//        public async Task EventIsPassedToHandlers()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler1 = new TestHandler(1);
//            await bus.Subscribe<TestEvent1>(i => handler1.Handle(i));
//            var handler2 = new TestHandler(1);
//            await bus.Subscribe<TestEvent1>(i => handler2.Handle(i));

//            // act
//            await bus.Publish(new TestEvent1());

//            await handler1.AllDone();
//            await handler2.AllDone();

//            // assert
//            Assert.Equal(1, handler1.Invocations);
//            Assert.Equal(1, handler2.Invocations);
//        }

//        [Fact]
//        public async Task MultipleEventsAreHandled()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler = new TestHandler(3);
//            await bus.Subscribe<TestEvent1>(i => handler.Handle(i));

//            // act
//            await bus.Publish(new TestEvent1());
//            await bus.Publish(new TestEvent1());
//            await bus.Publish(new TestEvent1());

//            await handler.AllDone();

//            // assert
//            Assert.Equal(3, handler.Invocations);
//        }

//        [Fact]
//        public async Task NoHandlersNoProblem()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();

//            // act
//            var ex = Act.Try(() => bus.Publish(new TestEvent1()));

//            await Task.Delay(100);

//            // assert
//            Assert.Null(ex);
//        }

//        [Fact]
//        public async Task UnknownEventIsNotHandled()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler = new TestHandler(0);
//            await bus.Subscribe<TestEvent1>(i => handler.Handle(i));

//            // act
//            await bus.Publish(new OtherTestEvent());

//            await handler.AllDone();

//            // assert
//            Assert.Equal(0, handler.Invocations);
//        }

//        [Fact]
//        public async Task UnsubscribedDoesNotAcceptEvents()
//        {
//            // arrange
//            var bus = new InMemoryEventBus();
//            var handler = new TestHandler(1);
//            var id = await bus.Subscribe<TestEvent1>(i => handler.Handle(i));

//            await bus.Publish(new TestEvent1());

//            await bus.UnSubscribe(id);

//            await bus.Publish(new TestEvent1());

//            await handler.AllDone();

//            // assert
//            Assert.Equal(1, handler.Invocations);
//        }
//    }
//}



