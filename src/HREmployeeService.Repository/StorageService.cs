using System.Threading.Tasks;
using HREmployeeService.Repository.Exceptions;
using HREmployeeService.Repository.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HREmployeeService.Repository
{
    public class StorageService : IStorageService
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DbName = "hr-employee";
        private readonly IMongoCollection<EmployeeData> _collection;

        public StorageService()
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DbName);
            _collection = db.GetCollection<EmployeeData>("employees");
        }

        public async Task<string> Create(object payload)
        {
            if (payload == null)
            {
                throw new MissingManatoryDataArgumentException("Create - Payload is null");
            }

            try
            {
                var id = ObjectId.GenerateNewId().ToString();
                await _collection.InsertOneAsync(new EmployeeData { Id = id, Payload = payload });

                return id;
            }
            catch (MongoException)
            {
                throw new StorageAccessException("Error creating Mongo document");
            }
        }

        public async Task<bool?> Update(string id, object payload)
        {
            if (id == null || payload == null)
            {
                throw new MissingManatoryDataArgumentException("Update - Supplied arguments is null");
            }

            try
            {
                var employeeData = await _collection.FindOneAndUpdateAsync(
                e => e.Id == id,
                Builders<EmployeeData>.Update.Set(e => e.Payload, payload));

                return employeeData.Id == id;
            }
            catch (MongoException)
            {
                throw new StorageAccessException("Error updating Mongo document");
            }
        }

        public async Task<object> Read(string id)
        {
            EmployeeData result;

            if (id == null)
            {
                throw new MissingManatoryDataArgumentException("Read - id is null");
            }

            try
            {
                var filter = Builders<EmployeeData>.Filter.Eq("_id", id);
                result = await _collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (MongoException)
            {
               throw new StorageAccessException("Error reading Mongo document");
            }

            return result.Payload;
        }
    }
}