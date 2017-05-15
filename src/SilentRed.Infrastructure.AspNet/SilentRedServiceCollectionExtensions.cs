using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Mediatr;
using SilentRed.Infrastructure.Query;
using SilentRed.Infrastructure.Runtime;
using SilentRed.Infrastructure.SimpleInjector;
using SilentRed.SimpleInjector.Extensions.Mediatr;
using SimpleInjector;

namespace SilentRed.Infrastructure.AspNet
{
    public static class SilentRedServiceCollectionExtensions
    {
        public static void AddSilentRed(this IServiceCollection services)
        {
            var container = new Container();
            var assemblies = AppDomain.GetAssemblies().ToList();

            container.ConfigureSilentRedWithSimpleInjector(assemblies);
            container.ConfigureSilentRedWithMediator(assemblies);

            container.RegisterSingleton<ICommandBus, MediatorCommandBus>();
            container.RegisterSingleton<IQueryBus, MediatorQueryBus>();
            //container.RegisterSingleton<INotificationBus, InMemoryNotificationBus>();

            services.AddSingleton<MvcQueryBus>();
            services.AddSingleton<MvcCommandBus>();
            //services.AddSingleton<MvcNotificationBus>();

        }
    }
}
