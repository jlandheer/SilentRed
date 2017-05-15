using System.Collections.Generic;
using System.Reflection;
using MediatR;
using SimpleInjector;

namespace SilentRed.SimpleInjector.Extensions.Mediatr
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterMediator(this Container container, IEnumerable<Assembly> assemblies)
        {
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);
            container.Register(typeof(ICancellableAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(ICancellableAsyncNotificationHandler<>), assemblies);

            container.RegisterCollection(typeof(IPipelineBehavior<,>), assemblies);
        }
    }
}
