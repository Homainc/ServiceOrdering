namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOperationResult
    {
        bool IsSucceed { get; }
        string ErrorMessage { get; }
    }
}
