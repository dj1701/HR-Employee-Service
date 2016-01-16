using MongoDB.Driver;

namespace HREmployeeService.Repository.Interfaces
{
    public interface IMongoProvider
    {
        IMongoDatabase GetDatabase(string dbName);
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}