using NUnit.Framework;

namespace HREmployeeService.Controllers.Tests
{
    public class PingControllerTests
    {
        [Test]
        public void ShouldReturnResponseWithPongMessage()
        {
            var unitUnderTest = new PingController();

            var result = unitUnderTest.Get();

            Assert.That(result.Content.ReadAsStringAsync().Result, Is.EqualTo("pong"));
        }
    }
}