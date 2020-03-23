using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories {
    public class UserRepository : IRepository<User>{
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext db) => _db = db;
        public IQueryable<User> GetAll() => _db.Users
            .Include(x => x.EmployeeProfile)
                .ThenInclude(x => x.ServiceType)
            .AsNoTracking().AsQueryable();
        public void Create(User user) => _db.Add(user);
        public void Update(User user) => _db.Update(user);
        public void Delete(User user) => _db.Remove(user);
    }
}