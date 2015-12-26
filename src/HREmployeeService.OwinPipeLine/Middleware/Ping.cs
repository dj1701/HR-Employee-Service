using System.Threading.Tasks;
using Microsoft.Owin;

namespace HREmployeeService.OwinPipeline.Middleware
{
    public class Ping : OwinMiddleware
    {
        public Ping(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            return context.Response.WriteAsync("pong");
        }
    }
}