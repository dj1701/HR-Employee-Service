using HREmployeeService.Controllers.Models;
using NUnit.Framework;

namespace HREmployeeService.Controllers.Tests
{
    [TestFixture]
    public class EmloyeeControllerTests
    {
        private EmployeeController _unitunderTest;

        [SetUp]
        public void SetUpBeforeEachTest()
        {
            _unitunderTest = new EmployeeController();
        }

        [Test]
        public void ShouldReturnResponseFromGetWithReadMessage()
        {
            const int employeeReferenceNumber = 1;
            var result = _unitunderTest.Get(employeeReferenceNumber);
            
            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("read"));
        }

        [Test]
        public void ShouldReturnResponseFromPostWithCreatedMessage()
        {
            var payload = new Payload {Data = new object()};

            var result = _unitunderTest.Post(payload);

            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("created"));
        }

        [Test]
        public void ShouldReturnResponseFromPutWithUpdatedMessage()
        {
            var payload = new Payload { Data = new object() };

            var result = _unitunderTest.Put(payload);

            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("updated"));
        }
    }
}
