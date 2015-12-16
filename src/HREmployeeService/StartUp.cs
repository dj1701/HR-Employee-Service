using HREmployeeService.OwinPipeline.Middleware;
using Owin;

namespace HREmployeeService
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/private/ping", x => x.Use<Ping>());
        }
    }
}