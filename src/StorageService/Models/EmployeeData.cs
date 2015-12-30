using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageService.Models
{
    public class EmployeeData
    {
        [BsonId]
        public string Id { get; set; }
        public object Payload { get; set; }
    }
}
