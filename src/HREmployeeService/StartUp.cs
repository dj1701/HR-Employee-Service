using System;
using HREmployeeService.OwinPipeline.Middleware;
using Owin;

namespace HREmployeeService
{
    public class StartUp : IDisposable
    {
        private bool _dispose = false;

        public StartUp()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled Exception Occurred");
        }

        public void Configuration(IAppBuilder app)
        {
            app.Map("/private/ping", x => x.Use<Ping>());
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