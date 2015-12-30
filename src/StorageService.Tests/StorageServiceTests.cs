using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace StorageService.Tests
{
    [TestFixture]
    public class StorageServiceTests
    {
        private IStorageService _unitUnderTest;
        private TestPayload _testPayload;

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            _unitUnderTest = new StorageService();

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
            var result = await _unitUnderTest.Create(_testPayload.Data);

            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<string>());
        }

        [Test, Ignore("")]
        public async Task ShouldReadEmployeeDataByReferenceNumber()
        {
            const string expectedEmployeeReference = "abc";
   
            await _unitUnderTest.Create(_testPayload.Data);
            var response = await _unitUnderTest.Read(expectedEmployeeReference);
            Assert.IsNotNull(response);

            //var result = JsonConvert.DeserializeObject(response.ToString());
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