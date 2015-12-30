using System.Net;
using System.Net.Http;
using System.Web.Http;
using HREmployeeService.Controllers.Models;

namespace HREmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        private string _badRequestResponseMessage = "bad request";

        [HttpGet]
        [Route("employee/read/{employeeReferenceNumber}")]
        public HttpResponseMessage Get(long employeeReferenceNumber)
        {
            //Read from mongo

            return new HttpResponseMessage { Content = new StringContent("read") };
        }

        [HttpPost]
        [Route("employee/create")]
        public HttpResponseMessage Post([FromBody] Payload value)
        {
            if (value?.Data == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(_badRequestResponseMessage),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            //Write to mongo

            return new HttpResponseMessage { Content = new StringContent("created") };
        }

        [HttpPut]
        [Route("employee/update")]
        public HttpResponseMessage Put([FromBody] Payload value)
        {
            if (value?.Data == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(_badRequestResponseMessage),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            //Write to mongo

            return new HttpResponseMessage { Content = new StringContent("updated") };
        }
    }
}