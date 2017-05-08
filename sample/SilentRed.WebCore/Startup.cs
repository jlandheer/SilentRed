using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SilentRed.Infrastructure;
using SilentRed.Infrastructure.Command;
using SilentRed.Infrastructure.Mediatr;
using SilentRed.Infrastructure.Runtime;
using SilentRed.Infrastructure.SimpleInjector;
using SilentRed.SimpleInjector.Extensions.Mediatr;
using SimpleInjector;

namespace SilentRed.WebCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var container = new Container();
            var assemblies = AppDomain.GetAssemblies().ToList();
            //assemblies.Add(typeof(NewCustomerCommand).GetTypeInfo().Assembly);

            container.ConfigureSilentRedWithSimpleInjector(assemblies);
            container.ConfigureSilentRedWithMediator(assemblies);

            container.RegisterSingleton<ICommandBus, MediatorCommandBus>();
            container.RegisterSingleton<IQueryBus, MediatorQueryBus>();

            container.RegisterSingleton<INotificationBus, InMemoryNotificationBus>();

            //var registrator = new SimpleInjectorRegistratorAndActivator(container);
            //registrator.ActivateAll();

            services.AddSingleton(container.GetInstance<INotificationBus>());
            services.AddSingleton(container.GetInstance<ICommandBus>());
            services.AddSingleton(container.GetInstance<IQueryBus>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
