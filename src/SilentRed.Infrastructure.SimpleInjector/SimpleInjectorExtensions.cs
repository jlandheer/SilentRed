using System.Collections.Generic;
using System.Reflection;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Runtime;
using SimpleInjector;

namespace SilentRed.Infrastructure.SimpleInjector
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterCommands(this Container container, IEnumerable<Assembly> assemblies = null)
        {
            var allAssemblies = assemblies ?? AppDomain.GetAssemblies();

            container.Register(typeof(ICommandHandler<>), allAssemblies);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandBusinessRulesValidation<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandAuthorization<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommandValidation<>));

            container.RegisterCollection(typeof(ICommandValidator<>), allAssemblies);
            container.RegisterCollection(typeof(ICommandAuthorizer<>), allAssemblies);
            container.RegisterCollection(typeof(ICommandBusinessRule<>), allAssemblies);
        }

        public static void RegisterQueries(this Container container, IEnumerable<Assembly> assemblies = null)
        {
            var allAssemblies = assemblies ?? AppDomain.GetAssemblies();

            container.Register(typeof(IQueryHandler<,>), allAssemblies);

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryValidation<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryAuthorization<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(QueryBusinessRulesValidation<,>));

            container.RegisterCollection(typeof(IQueryValidator<,>), allAssemblies);
            container.RegisterCollection(typeof(IQueryAuthorizer<,>), allAssemblies);
            container.RegisterCollection(typeof(IQueryBusinessRuleValidator<,>), allAssemblies);
        }
    }
}
