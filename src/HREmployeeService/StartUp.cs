using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HREmployeeService.Autofac;
using Owin;

namespace HREmployeeService
{
    public class StartUp : IDisposable
    {
        private bool _dispose = false;
        private readonly IContainer _container;

        public StartUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ControllerModule());

            _container = builder.Build();

            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled Exception Occurred");
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(_container);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            app.UseAutofacMiddleware(_container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void Dispose()
        {
            if (!_dispose)
            {
                AppDomain.CurrentDomain.UnhandledException -= UnhandledException;
            }

            _dispose = true;
        }
    }
}