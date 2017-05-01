using System;
using SilentRed.Infrastructure.SimpleInjector;
using SimpleInjector;
using Xunit;

namespace SilentRed.Infrastructure.Tests
{
    public class SimpleInjectorRegistratorAndActivatorTests
    {
        [Fact]
        public void ActivateAllCreatesAllTypes()
        {
            var container = new Container();
            container.Register<Counter>(Lifestyle.Singleton);

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.Register<TestService>();
            ac.Register<TestService2>();
            ac.ActivateAll();

            var counter = container.GetInstance<Counter>();
            Assert.Equal(2, counter.Count);
        }

        [Fact]
        public void ActivateAllCreatesInstance()
        {
            var container = new Container();
            container.Register<Counter>(Lifestyle.Singleton);

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.Register<TestService>();
            ac.ActivateAll();

            var counter = container.GetInstance<Counter>();
            Assert.Equal(1, counter.Count);
        }

        [Fact]
        public void CannotActivateMultipleTimes()
        {
            var container = new Container();

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.ActivateAll();
            var ex = Act.Try(() => ac.ActivateAll());

            Assert.IsType<InvalidOperationException>(ex);
        }

        [Fact]
        public void CannotRegisterAfterActivation()
        {
            var container = new Container();

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.ActivateAll();
            var ex = Act.Try(() => ac.Register<TestService>());

            Assert.IsType<InvalidOperationException>(ex);
        }

        [Fact]
        public void DependenciesAreInjected()
        {
            var container = new Container();
            container.Register<Counter>(Lifestyle.Singleton);

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.Register<TestService>();
            ac.Register<TestService2>();
            ac.Register<TestService3>();
            ac.ActivateAll();

            var counter = container.GetInstance<Counter>();
            Assert.Equal(3, counter.Count);

            var svc3 = container.GetInstance<TestService3>();
            Assert.NotNull(svc3.Service2);
        }

        [Fact]
        public void RegisterDoesNotActivate()
        {
            var container = new Container();
            container.Register<Counter>(Lifestyle.Singleton);

            var ac = new SimpleInjectorRegistratorAndActivator(container);

            ac.Register<TestService>();

            var counter = container.GetInstance<Counter>();
            Assert.Equal(0, counter.Count);
        }

        [Fact]
        public void RegisteredAsSingletons()
        {
            var container = new Container();
            container.Register<Counter>(Lifestyle.Singleton);

            var ac = new SimpleInjectorRegistratorAndActivator(container);
            ac.Register<TestService>();

            ac.ActivateAll();
            var i1 = container.GetInstance<TestService>();
            var i2 = container.GetInstance<TestService>();
            var i3 = container.GetInstance(typeof(TestService));

            Assert.Same(i1, i2);
            Assert.Same(i2, i3);

            var counter = container.GetInstance<Counter>();
            Assert.Equal(1, counter.Count);
        }

        internal class Counter
        {
            public void Inc() => Count++;

            public int Count { get; private set; }
        }

        internal class TestService
        {
            public TestService(Counter counter)
            {
                counter.Inc();
            }
        }

        internal class TestService2
        {
            public TestService2(Counter counter)
            {
                counter.Inc();
            }
        }

        internal class TestService3
        {
            public TestService2 Service2 { get; }

            public TestService3(Counter counter, TestService2 service2)
            {
                counter.Inc();
                Service2 = service2;
            }
        }
    }
}
