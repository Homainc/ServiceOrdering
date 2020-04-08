using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> GetByIdAsync(int id);
        Task<bool> AnyReviewByIdAsync(int id);
    }
}
