using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Common;
using OrderingService.Common.Interfaces;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ReviewRepository : AbstractRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : 
            base(db, httpContextAccessor) { }

        public override IQueryable<Review> GetAll() => Db.Reviews.AsQueryable();

        public async Task<Review> GetByIdOrDefaultAsync(int id) =>
            await Db.Reviews.SingleOrDefaultAsync(x => x.Id == id, Token);

        public async Task<IPagedResult<Review>> GetPagedEmployeeReviewsAsync(Guid employeeId, int pageSize, int pageNumber)
        {
            var query = 
                from r in Db.Reviews
                join u in Db.Users on r.ClientId equals u.Id into uGrouping
                from u in uGrouping.DefaultIfEmpty()
                orderby r.Date
                where r.EmployeeId == employeeId
                select new Review
                {
                    Id = r.Id,
                    EmployeeId = r.EmployeeId,
                    ClientId = r.ClientId,
                    Client = u,
                    Text = r.Text,
                    Date = r.Date,
                    Rate = r.Rate
                };
            var total = query.Count();

            return new PagedResult<Review>(
                await query.Paged(pageSize, pageNumber).ToListAsync(Token), total, pageSize, pageNumber);
        }

        public override void Create(Review entity)
        {
            base.Create(entity);

            var employee = Db.EmployeeProfiles.Single(x => x.Id == entity.EmployeeId);
            employee.AddReview(Db, entity);
            Db.Entry(employee).State = EntityState.Modified;
        }
    }
}
