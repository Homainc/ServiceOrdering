using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class EmployeeProfileRepository : AbstractRepository<EmployeeProfile>
    {
        public EmployeeProfileRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<EmployeeProfile> GetAll() => _db.EmployeeProfiles.AsQueryable();
    }
}
