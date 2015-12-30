using System.Threading.Tasks;

namespace StorageService
{
    public interface IStorageService
    {
        Task<string> Create(object payload);
        bool Update(object value);
        Task<object> Read(string referenceNumber);
    }
}
