using Autofac;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;
using NUnit.Framework;

namespace HREmployeeService.Autofac.Tests
{
    [TestFixture]
    public class RepositoryModuleTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUpBeforeTest()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryModule());

            _container = builder.Build();
        }

        [Test]
        public void resolves_RepositoryModule()
        {
            var result = _container.Resolve<IMongoProvider>();

            Assert.That(result, Is.InstanceOf<MongoProvider>());
        }
    }
}
