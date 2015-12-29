using Autofac;

namespace HREmployeeService.Autofac.Interfaces
{
    public interface IContainerFactory
    {
        IContainer Create();
    }
}
