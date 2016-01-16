using System;
using System.Threading.Tasks;
using System.Web.Http;
using HREmployeeService.Controllers.Models;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;

namespace HREmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IStorageService _storageService;
        private readonly string _badRequestResponseMessage = "bad request";

        public EmployeeController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        [Route("employee/{version}/{id}")]
        public async Task<IHttpActionResult> Get(string version, string id)
        {
            if (id == null || version == null)
            {
                return BadRequest(_badRequestResponseMessage);
            }

            var data = await _storageService.Read(version, id);

            return Ok(data);
        }

        [HttpPost]
        [Route("employee/{version}")]
        public async Task<IHttpActionResult> Post(string version, [FromBody] Payload value)
        {
            if (value?.Data == null || version == null)
            {
                return BadRequest(_badRequestResponseMessage);
            }

            var id = await _storageService.Create(version, value.Data);

            return Created(new Uri($"http://localhost:9000/employee/{version}/{id}"), id);
        }

        [HttpPut]
        [Route("employee/{version}/{id}")]
        public async Task<IHttpActionResult> Put(string version, string id, [FromBody] Payload value)
        {
            if (version == null || id == null || value?.Data == null)
            {
                return BadRequest(_badRequestResponseMessage);
            }

            var result = await _storageService.Update(version, id, value);
            if (!result)
            {
                return BadRequest("update failed");
            }

            return Ok();
        }
    }
}