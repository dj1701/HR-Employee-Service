using System.Threading.Tasks;

namespace HREmployeeService.Repository
{
    public interface IStorageService
    {
        Task<string> Create(object payload);
        Task<bool?> Update(string id, object payload);
        Task<object> Read(string id);
    }
}
