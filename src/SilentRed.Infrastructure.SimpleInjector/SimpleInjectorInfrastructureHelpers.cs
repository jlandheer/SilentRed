using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace SilentRed.Infrastructure.SimpleInjector
{
    public static class SimpleInjectorInfrastructureHelpers
    {
        public static Container ConfigureSilentRedWithSimpleInjector(Assembly assembly)
        {
            var container = new Container();
            return container.ConfigureSilentRedWithSimpleInjector(assembly);
        }

        public static Container ConfigureSilentRedWithSimpleInjector(IEnumerable<Assembly> assemblies = null)
        {
            var container = new Container();
            return container.ConfigureSilentRedWithSimpleInjector(assemblies);
        }

        public static Container ConfigureSilentRedWithSimpleInjector(this Container container, Assembly assembly)
        {
            return ConfigureSilentRedWithSimpleInjector(container, new[] { assembly });
        }

        public static Container ConfigureSilentRedWithSimpleInjector(
            this Container container,
            IEnumerable<Assembly> assemblies = null)
        {
            var allAssemblies = (assemblies ?? AppDomain.CurrentDomain.GetAssemblies()).ToList();

            container.RegisterCommands(allAssemblies);
            container.RegisterQueries(allAssemblies);

            return container;
        }
    }
}
