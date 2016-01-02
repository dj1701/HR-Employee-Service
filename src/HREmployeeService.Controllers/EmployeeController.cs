using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using HREmployeeService.Controllers.Models;
using HREmployeeService.Repository;

namespace HREmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IStorageService _storageService;
        private string _badRequestResponseMessage = "bad request";

        public EmployeeController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        [Route("employee/read/{id}")]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var data = await _storageService.Read(id);

            return new HttpResponseMessage { Content = new StringContent(data.ToString()) };
        }

        [HttpPost]
        [Route("employee/{version}")]
        public async Task<HttpResponseMessage> Post(string version, [FromBody] Payload value)
        {
            if (value?.Data == null)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(_badRequestResponseMessage),
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var id = await _storageService.Create(value.Data);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = new StringContent($"http://localhost:9000/employee/{version}/{id}")
            };
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