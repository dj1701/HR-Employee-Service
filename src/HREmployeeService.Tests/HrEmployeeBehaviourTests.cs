using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HREmployeeService.Tests
{
    [TestFixture]
    public class HrEmployeeBehaviourTests
    {
        private TestServer _server;
        private Employee _employee;

        [SetUp]
        public void SetUp()
        {
            _server = TestServer.Create<StartUp>();

            _employee = new Employee
            {
                Id = 1,
                EmployeeReference = "007",
                ForeName = "John",
                MiddleName = "George",
                Surname = "Smith",
                DateOfBirth = new DateTime(1980, 1, 1),
                EmployeeStartDate = new DateTime(2007, 1, 1),
                NationalInsurance = "1234",
                PlaceOfBirth = "UK",
                EmailAddress = "test@test.com",
                HomePhone = "01737111111",
                MobilePhone = "07845234233"
            };
        }

        [Test]
        public async Task ShouldHaveHttpResponseWithPongMessage()
        {
            var response = await _server.HttpClient.GetAsync("/private/ping");
            var result = await response.Content.ReadAsStringAsync();

            Assert.That("pong", Is.EqualTo(result));
        }

        [Test, Ignore("")]
        public async Task ShouldReceiveEmployeeRecordOnCreate()
        {
            var jsonString = JsonConvert.SerializeObject(_employee);
            var content = new StringContent(jsonString);
            var response = await _server.HttpClient.PostAsync("/employee/create", content);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseBodyContent, Is.EqualTo("Created"));
        }

        [Test, Ignore("")]
        public async Task ShouldFailToReceiveEmployeeRecordWithBlankStringOnCreate()
        {
            var response = await _server.HttpClient.PostAsync("/employee/create", new StringContent(string.Empty));
            var responseBodyContent = await response.Content.ReadAsStringAsync();
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("Bad Request"));
        }

        [Test, Ignore("")]
        public async Task ShouldFailToReceiveEmployeeRecordWithEmptyObjectOnCreate()
        {
            var response = await _server.HttpClient.PostAsync("/employee/create", null);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("Bad Request"));
        }

        [Test, Ignore("")]
        public async Task ShouldReceiveEmployeeRecordOnUpdate()
        {
            var jsonString = JsonConvert.SerializeObject(_employee);
            var content = new StringContent(jsonString);
            var response = await _server.HttpClient.PutAsync("/employee/update", content);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseBodyContent, Is.EqualTo("Updated"));
        }

        [Test, Ignore("")]
        public async Task ShouldFailToReceiveEmployeeRecordWithBlankStringOnUpdate()
        {
            var response = await _server.HttpClient.PutAsync("/employee/update", new StringContent(string.Empty));
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("Bad Request"));
        }

        [Test, Ignore("")]
        public async Task ShouldFailToReceiveEmployeeRecordWithEmptyObjectOnUpdate()
        {
            var response = await _server.HttpClient.PutAsync("/employee/update", new StreamContent(Stream.Null));
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("Bad Request"));
        }

        [Test, Ignore("")]
        public async Task ShouldFailToReceiveEmployeeRecordWithNullObjectOnUpdate()
        {
            var response = await _server.HttpClient.PutAsync("/employee/update", null);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("Bad Request"));
        }

        [Test, Ignore("")]
        public async Task ShouldReceiveEmployeeRecordOnRead()
        {
            const int expectedEmployeeReferenceNumber = 1;

            var response = await _server.HttpClient.GetAsync("/employee/read");
            var responseBodyContent = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<Employee>(responseBodyContent);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseBodyContent, Is.EqualTo("Read"));
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.EmployeeReference, Is.EqualTo(expectedEmployeeReferenceNumber));
        }
    }

    internal class Employee
    {
        public int Id { get; set; }
        public string EmployeeReference { get; set; }
        public string ForeName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EmployeeStartDate { get; set; }
        public DateTime EmployeeEndDate { get; set; }
        public string NationalInsurance { get; set; }
        public string PlaceOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
    }
}
