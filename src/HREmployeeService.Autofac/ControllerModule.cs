using Autofac;
using HREmployeeService.Controllers;

namespace HREmployeeService.Autofac
{
    public sealed class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PingController>();
            builder.RegisterType<EmployeeController>();
        }
    }
}