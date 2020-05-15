namespace OrderingService.Domain.Logic.Code.Exceptions
{
    public class NotFoundLogicException : FieldLogicException
    {
        public NotFoundLogicException(string message, string fieldName) : base(message, fieldName)
        {
        }
    }
}
