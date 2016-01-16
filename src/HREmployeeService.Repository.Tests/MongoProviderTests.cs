using HREmployeeService.Repository.Models;
using MongoDB.Driver;
using NUnit.Framework;

namespace HREmployeeService.Repository.Tests
{
    [TestFixture]
    public class MongoProviderTests
    {
        private MongoProvider _unitUnderTest;
        private IMongoDatabase _db;
        private const string DbName = "hr-employee";

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _unitUnderTest = new MongoProvider();

            _db = _unitUnderTest.GetDatabase(DbName);
        }

        [Test]
        public void ShouldGetMongoDatabase()
        {
            Assert.That(_db, Is.InstanceOf<IMongoDatabase>());
        }

        [Test]
        public void ShouldGetMongoDbCollection()
        {
            const string collectionName = "employees";

            var result = _unitUnderTest.GetCollection<EmployeeData>(collectionName);

            Assert.That(result, Is.InstanceOf<IMongoCollection<EmployeeData>>());
        }
    }
}
