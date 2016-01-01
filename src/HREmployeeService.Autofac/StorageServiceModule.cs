using System;
using Autofac;
using HREmployeeService.Repository;

namespace HREmployeeService.Autofac
{
    public class StorageServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StorageService>().As<IStorageService>();
        }
    }
}
