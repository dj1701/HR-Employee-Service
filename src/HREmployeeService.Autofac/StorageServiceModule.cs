﻿using Autofac;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;

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
