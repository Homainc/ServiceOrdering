using System;

namespace OrderingService.Domain.Logic.Code.Exceptions
{
    public class FieldLogicException : ArgumentException
    {
        public FieldLogicException(string message, string fieldName) : base(message, fieldName)
        {
        }
    }
}
