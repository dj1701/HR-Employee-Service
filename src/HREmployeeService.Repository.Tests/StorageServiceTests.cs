using System;
using System.Threading.Tasks;
using HREmployeeService.Repository.Exceptions;
using HREmployeeService.Repository.Interfaces;
using HREmployeeService.Repository.Tests.Helpers;
using HREmployeeService.Repository.Tests.Stubs;
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
        private string _version;
        private IMongoProvider _provider;
        private CollectionStub _conllection;

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            _conllection = new CollectionStub();
            _provider = new ProviderStub(_conllection);

            _unitUnderTest = new StorageService(_provider);
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

            _version = "1.0";
            _testPayload = new TestPayload {Data = jsonString};
        }

        [Test]
        public async Task ShouldCreateEmployeeDataRecordWithId()
        {
            ObjectId result;
            var resultId = await _unitUnderTest.Create(_version, _testPayload.Data);

            Assert.IsNotNull(resultId);
            Assert.That(ObjectId.TryParse(resultId, out result), Is.True);
        }

        [Test]
        public void ShouldRaiseMissingManatoryDataArgumentExceptionWhenIdIsNullOnCreate()
        {
            Assert.That(async () => await _unitUnderTest.Create(null, null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("Create - Supplied arguments is null"));
        }

        [Test]
        public async Task ShouldOnReadEmployeeDataByIdAndVersion()
        {
            var id = await _unitUnderTest.Create(_version, _testPayload.Data);

            var response = await _unitUnderTest.Read(_version, id);
            Assert.IsNotNull(response);

            var result = JObject.Parse(response.ToString());
            var actualEmployeeReference = (string)result.SelectToken("EmployeeReference");

            Assert.That(actualEmployeeReference, Is.EqualTo(_expectedEmployeeReference));
        }

        [Test]
        public void ShouldOnReadRaiseMissingManatoryDataArgumentExceptionWhenIdIsNull()
        {
            Assert.That(async () => await _unitUnderTest.Read(null, null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("Read - Supplied arguments is null"));
        }
        [Test]
        public async Task ShouldOnUpdateEmployeeDataHomeNumberByIdAndVersion()
        {
            var id = await _unitUnderTest.Create(_version, _testPayload.Data);
            var response = await _unitUnderTest.Read(_version, id);
            Assert.IsNotNull(response);

            var result = JObject.Parse(response.ToString());
            const string expectedHomeNumber = "0123456789";
            result["HomePhone"] = expectedHomeNumber;

            var updatedEmployeeData = result.ToString(Formatting.None);
            var payload = new TestPayload{ Data = updatedEmployeeData};
            var updated = await _unitUnderTest.Update(_version, id, payload.Data);

            Assert.That(updated, Is.EqualTo(true));
        }

        [Test]
        public async Task ShouldOnUpdateReturnFalseIfInvalidIdIsGiven()
        {
            await _unitUnderTest.Create(_version, _testPayload.Data);
            var payload = new TestPayload { Data = _testPayload };
            const string invalidId = "1234";
            var result = await _unitUnderTest.Update(_version, invalidId, payload.Data);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public async Task ShouldOnUpdateReturnFalseIfInvalidVersionIsGiven()
        {
            var payload = new TestPayload { Data = _testPayload };
            const string invalidVersion = "1234";

            var id = await _unitUnderTest.Create(_version, _testPayload.Data);

            _conllection.SetFindOneAndUpdateAsyncToReturnNull = true;
            var result = await _unitUnderTest.Update(invalidVersion, id, payload.Data);

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void ShouldOnUpdateRaiseMissingManatoryDataArgumentExceptionWhenIdIsNull()
        {
            Assert.That(async () => await _unitUnderTest.Update(null, null, null),
                Throws.TypeOf<MissingManatoryDataArgumentException>()
                    .With.Message.EqualTo("Update - Supplied arguments is null"));
        }
    }
}