using MongoDB.Bson.Serialization.Attributes;

namespace HREmployeeService.Repository.Models
{
    public class EmployeeData
    {
        [BsonId]
        public string Id { get; set; }
        public string Version { get; set; }
        public object Payload { get; set; }
    }
}
