using System;
using System.Collections.Generic;
using System.Text;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateUserToken(User user);
    }
}
