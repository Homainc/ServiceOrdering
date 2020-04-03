using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ReviewRepository : AbstractRepository<Review>
    {
        public ReviewRepository(ApplicationContext db) : base(db) { }

        public override IQueryable<Review> GetAll() => _db.Reviews.AsQueryable();
    }
}
