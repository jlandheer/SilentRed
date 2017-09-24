using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using SimpleInjector;

namespace SilentRed.SimpleInjector.Extensions.Mediatr
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterMediator(this Container container, IEnumerable<Assembly> assemblies)
        {
            var allAssemblies = (assemblies ?? new Assembly[0]).ToList();

            container.Register(typeof(IRequestHandler<,>), allAssemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), allAssemblies);
            container.Register(typeof(ICancellableAsyncRequestHandler<,>), allAssemblies);

            container.Register(typeof(IRequestHandler<>), allAssemblies);
            container.Register(typeof(IAsyncRequestHandler<>), allAssemblies);
            container.Register(typeof(ICancellableAsyncRequestHandler<>), allAssemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), allAssemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), allAssemblies);
            container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), allAssemblies);

            container.RegisterCollection(typeof(IPipelineBehavior<,>), allAssemblies);
        }
    }
}
