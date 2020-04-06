using System.Linq;
using Microsoft.AspNetCore.Http;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class EmployeeProfileRepository : AbstractRepository<EmployeeProfile>
    {
        public EmployeeProfileRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<EmployeeProfile> GetAll() => Db.EmployeeProfiles.AsQueryable();
    }
}
