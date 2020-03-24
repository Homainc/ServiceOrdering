using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories {
    public class UserRepository : AbstractRepository<User> 
    {
        public UserRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<User> GetAll() => _db.Users
            .Include(x => x.Role)
            .Include(x => x.EmployeeProfile)
                .ThenInclude(x => x.ServiceType)
            .AsNoTracking().AsQueryable();
    }
}