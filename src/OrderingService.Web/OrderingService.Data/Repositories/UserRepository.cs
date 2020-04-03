using System.Linq;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories {
    public class UserRepository : AbstractRepository<User> 
    {
        public UserRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<User> GetAll() => _db.Users.AsQueryable();
    }
}