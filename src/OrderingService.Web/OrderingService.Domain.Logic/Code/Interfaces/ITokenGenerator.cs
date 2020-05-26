using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Domain.Logic.Code.Interfaces
{
    public interface ITokenGenerator
    {
        Task<AccessTokenDto> CreateAccessTokenAsync(User user);
        Task<AccessTokenDto> RefreshAccessTokenAsync(AccessTokenDto accessToken);
    }
}
