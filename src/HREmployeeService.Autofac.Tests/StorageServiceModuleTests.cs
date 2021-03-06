﻿using Autofac;
using NUnit.Framework;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;

namespace HREmployeeService.Autofac.Tests
{
    [TestFixture]
    public class StorageServiceModuleTests : Module
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new StorageServiceModule());

            _container = builder.Build();
        }

        [Test]
        public void resolves_StorageServiceModule()
        {
            var result = _container.Resolve<IStorageService>();

            Assert.That(result, Is.InstanceOf<StorageService>());
        }
    }
}
