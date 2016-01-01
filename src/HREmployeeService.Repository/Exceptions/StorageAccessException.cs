using System;

namespace HREmployeeService.Repository.Exceptions
{
    public class StorageAccessException : Exception
    {
        public StorageAccessException(string message) : base(message)
        {
            
        }
    }
}
