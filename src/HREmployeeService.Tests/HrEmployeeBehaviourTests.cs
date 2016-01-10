using System;
using System.Collections.Generic;
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
        private TestClass _testClass;
        private string _payloadField;
        private string _payloadData;
        private Dictionary<string, string> _validPayLoad;
        private string _version;
        private HttpRequestMessage _request;

        [SetUp]
        public void SetUp()
        {
            _server = TestServer.Create<StartUp>();

            _testClass = new TestClass
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

            _version = "1.0";
            _payloadField = "Data";
            _payloadData = JsonConvert.SerializeObject(_testClass);
            _validPayLoad = new Dictionary<string, string> { { _payloadField, _payloadData } };

            _request = new HttpRequestMessage(HttpMethod.Post, $"/employee/{_version}")
            {
                Content = new FormUrlEncodedContent(_validPayLoad)
            };
        }

        [Test]
        public async Task ShouldHaveHttpResponseWithPongMessage()
        {
            var response = await _server.HttpClient.GetAsync("/private/ping");
            var result = await response.Content.ReadAsStringAsync();

            Assert.That("pong", Is.EqualTo(result));
        }

        [Test]
        public async Task ShouldReceiveEmployeeRecordOnCreate()
        {
            var response = await _server.HttpClient.SendAsync(_request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task ShouldFailToReceiveEmployeeRecordWithBlankStringOnCreate()
        {
            var inValidPayload = new Dictionary<string, string> { {string.Empty, string.Empty} };
            _request.Content = new FormUrlEncodedContent(inValidPayload);

            var response = await _server.HttpClient.SendAsync(_request);
            var responseBodyContent = await response.Content.ReadAsStringAsync();
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("{\"Message\":\"bad request\"}"));
        }

        [Test]
        public async Task ShouldFailToReceiveEmployeeRecordWithNoContentsOnCreate()
        {
            var inValidPayload = new Dictionary<string, string>();
            _request.Content = new FormUrlEncodedContent(inValidPayload);

            var response = await _server.HttpClient.SendAsync(_request);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("{\"Message\":\"bad request\"}"));
        }

        [Test]
        public async Task ShouldReceiveEmployeeRecordOnUpdate()
        {
            var resultFromCreate = await _server.HttpClient.SendAsync(_request);
            Assert.NotNull(resultFromCreate);

            var url = resultFromCreate.Headers.Location;
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(_validPayLoad)
            };

            var resultFromUpdate = await _server.HttpClient.SendAsync(request);
            Assert.That(resultFromUpdate.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task ShouldFailToReceiveEmployeeRecordWithBlankStringOnUpdate()
        {
            var resultFromCreate = await _server.HttpClient.SendAsync(_request);
            Assert.NotNull(resultFromCreate);

            var url = resultFromCreate.Headers.Location;
            var inValidPayload = new Dictionary<string, string> { { string.Empty, string.Empty } };
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(inValidPayload)
            };

            var response = await _server.HttpClient.SendAsync(request);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("{\"Message\":\"bad request\"}"));
        }

        [Test]
        public async Task ShouldFailToReceiveEmployeeRecordWithEmptyContentOnUpdate()
        {
            var resultFromCreate = await _server.HttpClient.SendAsync(_request);
            Assert.NotNull(resultFromCreate);

            var url = resultFromCreate.Headers.Location;
            
            var inValidPayload = new Dictionary<string, string>();
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new FormUrlEncodedContent(inValidPayload)
            };

            var response = await _server.HttpClient.SendAsync(request);
            var responseBodyContent = await response.Content.ReadAsStringAsync();

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseBodyContent, Is.EqualTo("{\"Message\":\"bad request\"}"));
        }

        [Test, Ignore("")]
        public async Task ShouldReceiveEmployeeRecordOnRead()
        {
            var requestToCreate = new HttpRequestMessage(HttpMethod.Post, $"/employee/{_version}")
            {
                Content = new FormUrlEncodedContent(_validPayLoad)
            };

            var result = await _server.HttpClient.SendAsync(requestToCreate);

            var id = result.Content.ReadAsStringAsync().Result;

            var response = await _server.HttpClient.GetAsync($"/employee/{_version}/{id}");
            var responseBodyContent = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<TestClass>(responseBodyContent);
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(jsonResult);
            Assert.That(jsonResult.EmployeeReference, Is.EqualTo(_testClass.EmployeeReference));
        }
    }

    internal class TestClass
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
