using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace HREmployeeService.Tests
{
    [TestFixture]
    public class HrEmployeeServiceTests
    {
        private TestServer _server;

        [SetUp]
        public void SetUp()
        {
            _server = TestServer.Create<StartUp>();
        }

        [Test]
        public void ShouldHaveHttpResponseWithPongMessage()
        {
            var response = _server.HttpClient.GetAsync("/private/ping").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.That("pong", Is.EqualTo(result));
        }
    }
}
