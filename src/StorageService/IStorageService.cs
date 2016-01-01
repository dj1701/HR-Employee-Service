using System.Threading.Tasks;
using System.Web.Http;

namespace StorageService
{
    public interface IStorageService
    {
        Task<string> Create(object payload);
        Task<bool?> Update(string id, object payload);
        Task<object> Read(string id);
    }
}
