using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class EmployeeProfileRepository : IRepository<EmployeeProfile>
    {
        private readonly ApplicationContext _db;
        public EmployeeProfileRepository(ApplicationContext appContext)
        {
            _db = appContext;
        }

        public IQueryable<EmployeeProfile> GetAll() => _db.EmployeeProfiles
            .Include(e => e.User)
            .Include(e => e.ServiceType)
            .AsNoTracking().AsQueryable();

        public void Create(EmployeeProfile entity) => _db.Add(entity);

        public void Update(EmployeeProfile entity) => _db.Entry(entity).State = EntityState.Modified;

        public void Delete(EmployeeProfile entity)
        {
            var employeeProfile = _db.EmployeeProfiles.Find(entity.Id);
            if (employeeProfile != null)
                _db.EmployeeProfiles.Remove(employeeProfile);
        }
    }
}
