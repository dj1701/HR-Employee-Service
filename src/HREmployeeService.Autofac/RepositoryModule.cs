using Autofac;
using HREmployeeService.Repository;
using HREmployeeService.Repository.Interfaces;

namespace HREmployeeService.Autofac
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoProvider>().As<IMongoProvider>();
        }
    }
}