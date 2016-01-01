using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using HREmployeeService.Controllers.Models;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Exceptions;

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
            if (id == null)
            {
                throw new MissingManatoryDataArgumentException("id is null");
            }

            var data = await _storageService.Read(id);
            if (data == null)
            {
                return new HttpResponseMessage
                {
                    Content = null,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new HttpResponseMessage { Content = new StringContent(data.ToString()) };
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