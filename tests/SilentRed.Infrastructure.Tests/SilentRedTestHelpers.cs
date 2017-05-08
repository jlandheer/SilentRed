using System.Collections.Generic;
using System.Reflection;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Mediatr;
using SilentRed.Infrastructure.SimpleInjector;
using SilentRed.SimpleInjector.Extensions.Mediatr;
using SimpleInjector;

namespace SilentRed.Infrastructure.Tests
{
    public static class SilentRedTestHelpers
    {
        public static Container GetContainer(Assembly assembly)
        {
            return GetContainer(new[] { assembly });
        }

        public static Container GetContainer(IEnumerable<Assembly> assemblies)
        {
            var container = new Container()
                .ConfigureSilentRedWithMediator(assemblies)
                .ConfigureSilentRedWithSimpleInjector(assemblies);

            container.RegisterSingleton<ICommandBus, MediatorCommandBus>();
            container.RegisterSingleton<IQueryBus, MediatorQueryBus>();
            container.RegisterSingleton<INotificationBus, InMemoryNotificationBus>();

            return container;
        }
    }
}
