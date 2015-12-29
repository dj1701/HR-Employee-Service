using System.Net.Http;
using System.Web.Http;

namespace HREmployeeService.Controllers
{
    public class PingController : ApiController
    {
        [HttpGet]
        [Route("private/ping")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("pong")
            };
        }
    }
}