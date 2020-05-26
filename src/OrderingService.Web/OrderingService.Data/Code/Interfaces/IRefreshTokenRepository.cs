using System;
using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Code.Interfaces
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        public Task<RefreshToken> GetByUserIdOrDefaultAsync(Guid id);
        public Task<bool> AnyByUserIdAsync(Guid id);
    }
}
