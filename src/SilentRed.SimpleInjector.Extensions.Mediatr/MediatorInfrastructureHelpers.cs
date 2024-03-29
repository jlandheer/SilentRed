using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using SilentRed.Infrastructure.Mediatr;
using SilentRed.Infrastructure.Runtime;
using SimpleInjector;

namespace SilentRed.SimpleInjector.Extensions.Mediatr
{
    public static class MediatorInfrastructureHelpers
    {
        public static Container ConfigureSilentRedWithMediator(Assembly assembly)
        {
            var container = new Container();
            return container.ConfigureSilentRedWithMediator(assembly);
        }

        public static Container ConfigureSilentRedWithMediator(IEnumerable<Assembly> assemblies = null)
        {
            var container = new Container();
            return container.ConfigureSilentRedWithMediator(assemblies);
        }

        public static Container ConfigureSilentRedWithMediator(this Container container, Assembly assembly)
        {
            return ConfigureSilentRedWithMediator(container, new[] { assembly });
        }

        public static Container ConfigureSilentRedWithMediator(
            this Container container,
            IEnumerable<Assembly> assemblies = null)
        {
            var allAssemblies = (assemblies ?? AppDomain.GetAssemblies()).ToList();

            container.RegisterSingleton<IMediator>(() => new Mediator(container.GetInstance, container.GetAllInstances));

            container.RegisterMediator(allAssemblies);
            container.Register(
                typeof(ICancellableAsyncRequestHandler<>),
                typeof(CommandHandlerWrappedForMediator<>));

            container.Register(
                typeof(ICancellableAsyncRequestHandler<,>),
                typeof(QueryHandlerWrappedForMediator<,>));

            return container;
        }
    }
}
