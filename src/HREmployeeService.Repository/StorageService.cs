using System.Threading.Tasks;
using HREmployeeService.Repository.Exceptions;
using HREmployeeService.Repository.Interfaces;
using HREmployeeService.Repository.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HREmployeeService.Repository
{
    public class StorageService : IStorageService
    {
        private const string DbName = "hr-employee";
        private const string CollectionName = "employees";
        private readonly IMongoCollection<EmployeeData> _collection;

        public StorageService(IMongoProvider provider)
        {
            provider.GetDatabase(DbName);
            _collection = provider.GetCollection<EmployeeData>(CollectionName);
        }

        public async Task<string> Create(string version, object payload)
        {
            if (version == null || payload == null)
            {
                throw new MissingManatoryDataArgumentException("Create - Supplied arguments is null");
            }

            try
            {
                var id = ObjectId.GenerateNewId().ToString();
                await _collection.InsertOneAsync(new EmployeeData { Id = id, Version = version, Payload = payload });

                return id;
            }
            catch (MongoException)
            {
                throw new StorageAccessException("Error creating Mongo document");
            }
        }

        public async Task<bool> Update(string version, string id, object payload)
        {
            if (id == null || payload == null)
            {
                throw new MissingManatoryDataArgumentException("Update - Supplied arguments is null");
            }

            try
            {
                var employeeData = await _collection.FindOneAndUpdateAsync(
                e => e.Id == id && e.Version == version,
                Builders<EmployeeData>.Update.Set(e => e.Payload, payload).Set(e => e.Version, version));

                return employeeData?.Id == id;
            }
            catch (MongoException)
            {
                throw new StorageAccessException("Error updating Mongo document");
            }
        }

        public async Task<object> Read(string version, string id)
        {
            EmployeeData result;

            if (id == null || version == null)
            {
                throw new MissingManatoryDataArgumentException("Read - Supplied arguments is null");
            }

            try
            {
                result = await _collection.FindAsync(x => x.Version == version && x.Id == id).Result.FirstOrDefaultAsync();
            }
            catch (MongoException)
            {
               throw new StorageAccessException("Error reading Mongo document");
            }

            return result.Payload;
        }
    }
}