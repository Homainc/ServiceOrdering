using System;

namespace OrderingService.Domain.Logic.Code.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        {
            
        }
    }
}