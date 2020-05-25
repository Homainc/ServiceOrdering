using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Code.Abstractions;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class RoleRepository : AbstractRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public async Task<int> GetRoleIdByNameAsync(string roleName) =>
            (await Db.Roles.SingleAsync(x => x.Name == roleName, Token)).Id;
    }
}