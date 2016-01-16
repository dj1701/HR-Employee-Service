using HREmployeeService.Repository.Interfaces;
using MongoDB.Driver;

namespace HREmployeeService.Repository
{
    public class MongoProvider : IMongoProvider
    {
        private IMongoDatabase _db;

        public IMongoDatabase GetDatabase(string dbName)
        {
            const string connectionString = "mongodb://localhost";

            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(dbName);
            return _db;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _db.GetCollection<T>(collectionName);
        }
    }
}