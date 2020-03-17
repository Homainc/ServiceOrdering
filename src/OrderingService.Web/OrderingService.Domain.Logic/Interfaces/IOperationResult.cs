using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingService.Domain.Logic.Interfaces
{
    public interface IOperationResult
    {
        bool IsSucceed { get; }
        string ErrorMessage { get; }
    }
}
