using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class EmployeeProfileRepository : AbstractRepository<EmployeeProfile>, IEmployeeRepository
    {
        public EmployeeProfileRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<EmployeeProfile> GetAll() => Db.EmployeeProfiles.AsQueryable();

        public async Task<bool> AnyEmployeeAsync(Expression<Func<EmployeeProfile, bool>> filter) =>
            await Db.EmployeeProfiles.AnyAsync(filter, Token);

        public async Task<EmployeeProfile> EagerSingleAsync(Expression<Func<EmployeeProfile, bool>> filter) =>
            await (
                from e in Db.EmployeeProfiles
                join u in Db.Users on e.UserId equals u.Id
                join st in Db.ServiceTypes on e.ServiceTypeId equals st.Id
                select new EmployeeProfile
                {
                    Id = e.Id,
                    ServiceTypeId = st.Id,
                    ServiceType = st,
                    ServiceCost = e.ServiceCost,
                    UserId = u.Id,
                    User = u,
                    Description = e.Description
                }).SingleAsync(filter, Token);
    }
}
