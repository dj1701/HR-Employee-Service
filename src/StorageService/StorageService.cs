using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using StorageService.Models;

namespace StorageService
{
    public class StorageService : IStorageService
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DbName = "hr-employee";
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<EmployeeData> _collection;

        public StorageService()
        {
            var client = new MongoClient(ConnectionString);
            _db = client.GetDatabase(DbName);
            _collection = _db.GetCollection<EmployeeData>("employees");
        }

        public async Task<string> Create(object payload)
        {
            if (payload == null)
            {
                //log error here
                return null;
            }

            try
            {
                var id = ObjectId.GenerateNewId().ToString();
                await _collection.InsertOneAsync(new EmployeeData { Id = id, Payload = payload });

                return id;
            }
            catch (MongoException)
            {
                throw new Exception("Error creating Mongo document");
            }
        }

        public bool Update(object value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<object> Read(string referenceNumber)
        {
            var filter = Builders<EmployeeData>.Filter.Eq("EmployeeReference", referenceNumber);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            return result?.Payload;
        }
    }
}