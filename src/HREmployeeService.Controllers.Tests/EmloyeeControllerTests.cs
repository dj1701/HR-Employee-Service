using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using FakeItEasy;
using HREmployeeService.Controllers.Models;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;
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
        private string _expectedCreatedId;
        private Payload _payload;
        private string _version;

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

            _payload = new Payload {Data = _expectedPayloadData};
            _expectedCreatedId = "123456789";
            _version = "1.0";
            A.CallTo(() => _storageService.Create(_version, _payload.Data)).Returns(_expectedCreatedId);
        }

        [Test]
        public async Task ShouldOnCreateReturnResponseWithUrl()
        {
            var expectedUrl = $"http://localhost:9000/employee/{_version}/{_expectedCreatedId}";
            var result = await _unitUnderTest.Post(_version, _payload);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<CreatedNegotiatedContentResult<string>>());
            Assert.That(((CreatedNegotiatedContentResult<string>)result).Content, Is.EqualTo(_expectedCreatedId));
            Assert.That(((CreatedNegotiatedContentResult<string>)result).Location.AbsoluteUri, Is.EqualTo(expectedUrl));
        }

        [Test]
        public async Task ShouldOnCreateReceiveBadRequestResponseWhenEitherVersionOrPayloadArgumentsIsNull()
        {
            var result = await _unitUnderTest.Post(null, null);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestErrorMessageResult>());
            Assert.That(((BadRequestErrorMessageResult)result).Message, Is.EqualTo("bad request"));
        }

        [Test]
        public async Task ShouldOnReadReturnResponseWithPayload()
        {
            const string id = "1";

            A.CallTo(() => _storageService.Read(_version, id)).Returns(_expectedPayloadData);
            var result = await _unitUnderTest.Get(_version, id);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkNegotiatedContentResult<object>>());
            Assert.That(((OkNegotiatedContentResult<object>)result).Content, Is.EqualTo(_expectedPayloadData));
        }

        [Test]
        public async Task ShouldOnReadReceiveBadRequestWhenIdIsNull()
        {
            var result = await _unitUnderTest.Get(null, null);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestErrorMessageResult>());
            Assert.That(((BadRequestErrorMessageResult)result).Message, Is.EqualTo("bad request"));
        }

        [Test]
        public async Task ShouldOnUpdateReceiveOkStatusCodeResponseFromPutWhenSuccessfullyUpdated()
        {
            A.CallTo(() => _storageService.Update(_version, _expectedCreatedId, _payload)).Returns(true);
            var result = await _unitUnderTest.Put(_version, _expectedCreatedId, _payload);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<OkResult>());
        }

        [Test]
        public async Task ShouldOnUpdateReceiveBadRequestIfUpdateFailsWithIncorrectId()
        {
            const string noExistentId = "1234";
            var result = await _unitUnderTest.Put(_version, noExistentId, _payload);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestErrorMessageResult>());
            Assert.That(((BadRequestErrorMessageResult)result).Message, Is.EqualTo("update failed"));
        }

        [Test]
        public async Task ShouldOnUpdateReceiveBadRequestWhenEitherArgumentsIsNull()
        {
            var result = await _unitUnderTest.Put(null, null, null);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<BadRequestErrorMessageResult>());
            Assert.That(((BadRequestErrorMessageResult)result).Message, Is.EqualTo("bad request"));
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
