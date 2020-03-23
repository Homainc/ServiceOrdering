using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly ApplicationContext _db;
        public ReviewRepository(ApplicationContext appContext)
        {
            _db = appContext;
        }

        public IQueryable<Review> GetAll() => _db.Reviews
            .AsNoTracking().AsQueryable();

        public void Create(Review entity) => _db.Add(entity);

        public void Update(Review entity) => _db.Entry(entity).State = EntityState.Modified;

        public void Delete(Review entity)
        {
            var review = _db.Reviews.Find(entity.Id);
            if (review != null)
                _db.Reviews.Remove(review);
        }
    }
}
