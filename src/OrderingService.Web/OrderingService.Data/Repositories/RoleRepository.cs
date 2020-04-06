using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OrderingService.Data.EF;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories
{
    public class RoleRepository : AbstractRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<Role> GetAll() => Db.Roles.AsQueryable();

        public async Task<int> GetRoleIdByNameAsync(string roleName) =>
            (await Db.Roles.SingleAsync(x => x.Name == roleName, Token)).Id;
    }
}