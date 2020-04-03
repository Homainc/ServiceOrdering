using System.Linq;
using OrderingService.Data.EF;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories
{
    public class RoleRepository : AbstractRepository<Role>
    {
        public RoleRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<Role> GetAll() => _db.Roles.AsQueryable();
    }
}