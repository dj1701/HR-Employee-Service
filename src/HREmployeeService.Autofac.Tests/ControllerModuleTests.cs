using Autofac;
using Autofac.Core;
using HREmployeeService.Controllers;
using NUnit.Framework;

namespace HREmployeeService.Autofac.Tests
{
    public class ControllerModuleTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ControllerModule());

            _container = builder.Build();
        }

        [Test]
        public void resolves_PingController()
        {
            var result = _container.Resolve<PingController>();

            Assert.That(result, Is.InstanceOf<PingController>());
        }

        [Test]
        public void resolves_EmployeeController()
        {
            var result = _container.Resolve<EmployeeController>();

            Assert.That(result, Is.InstanceOf<EmployeeController>());
        }
    }
}
