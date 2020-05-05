using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Binbin.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Common;
using OrderingService.Common.Interfaces;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class EmployeeProfileRepository : AbstractRepository<EmployeeProfile>, IEmployeeRepository
    {
        public EmployeeProfileRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor) { }

        public override IQueryable<EmployeeProfile> GetAll() => Db.EmployeeProfiles.AsQueryable();

        public async Task<bool> AnyEmployeeAsync(Expression<Func<EmployeeProfile, bool>> filter) =>
            await Db.EmployeeProfiles.AnyAsync(filter, Token);

        public async Task<EmployeeProfile> GetEagerByIdOrDefaultAsync(Guid id) =>
            await (
                from e in Db.EmployeeProfiles
                join u in Db.Users on e.UserId equals u.Id
                join st in Db.ServiceTypes on e.ServiceTypeId equals st.Id
                select new EmployeeProfile(e.AverageRate, e.ReviewsCount)
                {
                    Id = e.Id,
                    ServiceTypeId = st.Id,
                    ServiceType = st,
                    ServiceCost = e.ServiceCost,
                    UserId = u.Id,
                    User = u,
                    Description = e.Description
                }).SingleOrDefaultAsync(x => x.Id == id, Token);

        public async Task<IPagedResult<EmployeeProfile>> GetPagedEmployeesAsync(int pageSize, int pageNumber,
            string searchString = null, decimal? maxServiceCost = null, int? minAverageRate = null, int? serviceTypeId = null)
        {
            var employeeFilter = PredicateBuilder.True<EmployeeProfile>();
            if (maxServiceCost.HasValue)
                employeeFilter = employeeFilter.And(x => x.ServiceCost <= maxServiceCost.Value);
            if (minAverageRate.HasValue)
                employeeFilter = employeeFilter.And(x => x.AverageRate >= minAverageRate.Value);
            if (!string.IsNullOrEmpty(searchString))
                employeeFilter = employeeFilter.And(x => x.Description.Contains(searchString));

            var serviceTypeFilter = PredicateBuilder.True<ServiceType>();
            if (serviceTypeId.HasValue)
                serviceTypeFilter = serviceTypeFilter.And(x => x.Id == serviceTypeId.Value);

            var query =
                from e in Db.EmployeeProfiles.Where(employeeFilter)
                join u in Db.Users on e.UserId equals u.Id into uGrouping
                from u in uGrouping.DefaultIfEmpty()
                join st in Db.ServiceTypes.Where(serviceTypeFilter) on e.ServiceTypeId equals st.Id
                select new EmployeeProfile(e.AverageRate, e.ReviewsCount)
                {
                    Id = e.Id,
                    ServiceTypeId = e.ServiceTypeId,
                    ServiceType = st,
                    ServiceCost = e.ServiceCost,
                    UserId = e.UserId,
                    User = u,
                    Description = e.Description
                };

            var total = query.Count();

            return new PagedResult<EmployeeProfile>(await query.Paged(pageSize, pageNumber).ToListAsync(Token), total,
                pageSize, pageNumber);
        }

        public async Task<Guid> GetUserIdByEmployeeIdAsync(Guid employeeId) =>
            await (from e in Db.EmployeeProfiles.Where(x => x.Id == employeeId) select e.UserId).SingleAsync(Token);
    }
}
