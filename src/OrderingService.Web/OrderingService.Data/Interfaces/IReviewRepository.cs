using System;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<Review> GetByIdAsync(int id);
        Task<bool> AnyReviewByIdAsync(int id);
        Task<IPagedResult<Review>> GetPagedEmployeeReviewsAsync(Guid employeeId, int pageSize, int pageNumber);
    }
}
