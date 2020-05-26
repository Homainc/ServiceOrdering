using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Code.Abstractions;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    class RefreshTokenRepository : AbstractRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor)
        {
        }

        public async Task<RefreshToken> GetByUserIdOrDefaultAsync(Guid id) =>
            await Db.RefreshTokens.SingleOrDefaultAsync(x => x.UserId == id, Token);

        public async Task<bool> AnyByUserIdAsync(Guid id) =>
            await Db.RefreshTokens.AnyAsync(x => x.UserId == id, Token);
    }
}
