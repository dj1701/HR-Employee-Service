using System;
using System.Threading.Tasks;
using HREmployeeService.Repository.Exceptions;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace HREmployeeService.Repository.Tests
{
    [TestFixture]
    public class StorageServiceTests
    {
        private IStorageService _unitUnderTest;
        private TestPayload _testPayload;
        private string _expectedEmployeeReference;

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            _unitUnderTest = new StorageService();
            _expectedEmployeeReference = "abc";

            var testClass = new TestClass
            {
                Id = 1,
                EmployeeReference = "abc",
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

            var jsonString = JsonConvert.SerializeObject(testClass);

            _testPayload = new TestPayload {Data = jsonString};
        }

        [Test]
        public async Task ShouldCreateEmployeeDataRecordWithId()
        {
            ObjectId result;
            var resultId = await _unitUnderTest.Create(_testPayload.Data);

            Assert.IsNotNull(resultId);
            Assert.That(ObjectId.TryParse(resultId, out result), Is.True);
        }

        [Test]
        public void ShouldRaiseMissingManatoryDataArgumentExceptionWhenIdIsNullOnCreate()
        {
            Assert.That(async () => await _unitUnderTest.Create(null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("Payload is null"));
        }

        [Test]
        public async Task ShouldOnReadEmployeeDataByIdWithEmployeeReferenceNumber()
        {
            var id = await _unitUnderTest.Create(_testPayload.Data);
            var response = await _unitUnderTest.Read(id);
            Assert.IsNotNull(response);

            var result = JObject.Parse(response.ToString());
            var actualEmployeeReference = (string)result.SelectToken("EmployeeReference");

            Assert.That(actualEmployeeReference, Is.EqualTo(_expectedEmployeeReference));
        }

        [Test]
        public void ShouldOnReadRaiseMissingManatoryDataArgumentExceptionWhenIdIsNull()
        {
            Assert.That(async () => await _unitUnderTest.Read(null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("id is null"));
        }
        [Test]
        public async Task ShouldOnUpdateEmployeeDataHomeNumberById()
        {
            var id = await _unitUnderTest.Create(_testPayload.Data);
            var response = await _unitUnderTest.Read(id);
            Assert.IsNotNull(response);

            var result = JObject.Parse(response.ToString());
            const string expectedHomeNumber = "0123456789";
            result["HomePhone"] = expectedHomeNumber;

            var updatedEmployeeData = result.ToString(Formatting.None);
            var payload = new TestPayload{ Data = updatedEmployeeData};
            var updated = await _unitUnderTest.Update(id, payload);

            Assert.That(updated, Is.EqualTo(true));
        }

        [Test]
        public void ShouldOnUpdateRaiseMissingManatoryDataArgumentExceptionWhenIdIsNull()
        {
            Assert.That(async () => await _unitUnderTest.Update(null, null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("Supplied arguments is null"));
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

    internal class TestPayload
    {
        public object Data { get; set; }
    }
}