using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ReviewRepository : AbstractRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<Review> GetAll() => Db.Reviews.AsQueryable();

        public async Task<Review> GetByIdAsync(int id) =>
            await Db.Reviews.SingleAsync(x => x.Id == id, Token);

        public async Task<bool> AnyReviewByIdAsync(int id) =>
            await Db.Reviews.AnyAsync(x => x.Id == id, Token);
    }
}
