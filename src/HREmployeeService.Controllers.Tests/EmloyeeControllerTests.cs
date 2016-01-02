using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using HREmployeeService.Controllers.Models;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Exceptions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace HREmployeeService.Controllers.Tests
{
    [TestFixture]
    public class EmloyeeControllerTests
    {
        private EmployeeController _unitUnderTest;
        private IStorageService _storageService;
        private TestClass _testClass;
        private string _expectedPayloadData;

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            _storageService = A.Fake<IStorageService>();
            _unitUnderTest = new EmployeeController(_storageService);

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

            _expectedPayloadData = JsonConvert.SerializeObject(_testClass);
        }

        [Test]
        public async Task ShouldOnReadReturnResponseWithPayload()
        {
            const string id = "1";

            A.CallTo(() => _storageService.Read(id)).Returns(_expectedPayloadData);
            var result = await _unitUnderTest.Get(id);
            
            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo(_expectedPayloadData));
        }

        [Test]
        public void ShouldOnReadReceiveMissingManatoryDataArgumentExceptionWhenIdIsNull()
        {
            A.CallTo(() => _storageService.Read(null)).Throws(new MissingManatoryDataArgumentException("id is null"));

            Assert.That(async () => await _unitUnderTest.Get(null),
               Throws.TypeOf<MissingManatoryDataArgumentException>()
                   .With.Message.EqualTo("id is null"));
        }

        [Test]
        public async Task ShouldOnCreateReturnResponseWithUrl()
        {
            var payload = new Payload {Data = _expectedPayloadData};

            const string version = "1.0";
            const string fakeId = "123456789";
            A.CallTo(() => _storageService.Create(payload.Data)).Returns(fakeId);
            var result = await _unitUnderTest.Post(version, payload);
            
            var expectedUrl = $"http://localhost:9000/employee/{version}/{fakeId}";
            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo(expectedUrl));
        }

        [Test]
        public async Task ShouldOnUpdateReceiveBadRequestWhenEitherVersionOrPayloadIsNull()
        {
            var result = await _unitUnderTest.Post(null, null);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("bad request"));
        }

        [Test]
        public void ShouldReturnResponseFromPutWithUpdatedMessage()
        {
            var payload = new Payload { Data = new object() };

            var result = _unitUnderTest.Put(payload);

            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("updated"));
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
