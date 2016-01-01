using System;

namespace HREmployeeService.Repository.Exceptions
{
    public class MissingManatoryDataArgumentException : Exception
    {
        public MissingManatoryDataArgumentException(string message) : base(message)
        {
            
        }
    }
}