using System.Linq;
using OrderingService.Data.EF;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories {
    public class RoleRepository : IRepository<Role>{
        private readonly ApplicationContext _db;
        public RoleRepository(ApplicationContext db) => _db = db;
        public IQueryable<Role> GetAll() => _db.Roles
            .AsNoTracking().AsQueryable();
        public void Create(Role role) => _db.Add(role);
        public void Update(Role role) => _db.Update(role);
        public void Delete(Role role) => _db.Remove(role);
    }
}