using System;
using System.Collections.Generic;
using System.Text;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Domain.Logic.Services
{
    public class OperationResult : IOperationResult
    {
        public bool IsSucceed { get; }
        public string ErrorMessage { get; }
        public OperationResult()
        {
            IsSucceed = true;
        }
        public OperationResult(string errorMessage)
        {
            IsSucceed = false;
            ErrorMessage = errorMessage;
        }
        public static OperationResult Success() => new OperationResult();
        public static OperationResult Error(string msg) => new OperationResult(msg);
    }
}
