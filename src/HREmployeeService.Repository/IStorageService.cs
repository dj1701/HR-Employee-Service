using System.Reflection.Emit;
using System.Threading.Tasks;

namespace HREmployeeService.Repository
{
    public interface IStorageService
    {
        Task<string> Create(string version, object payload);
        Task<bool> Update(string version, string id, object payload);
        Task<object> Read(string version, string id);
    }
}
