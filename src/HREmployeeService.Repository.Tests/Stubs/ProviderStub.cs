using System;
using HREmployeeService.Repository.Interfaces;
using HREmployeeService.Repository.Models;
using MongoDB.Driver;

namespace HREmployeeService.Repository.Tests.Stubs
{
    public class ProviderStub : IMongoProvider
    {
        private readonly IMongoCollection<EmployeeData> _collectionStub;
        private MongoDatabaseStub _db;

        public ProviderStub(IMongoCollection<EmployeeData> collectionStub)
        {
            _collectionStub = collectionStub;
        }

        public IMongoDatabase GetDatabase(string dbName)
        {
            _db = new MongoDatabaseStub();
            return _db;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return (IMongoCollection<T>) _collectionStub;
        }
    }
}