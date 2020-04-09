using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Common.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeProfile>
    {
        Task<bool> AnyEmployeeAsync(Expression<Func<EmployeeProfile, bool>> filter);
        Task<EmployeeProfile> EagerSingleAsync(Expression<Func<EmployeeProfile, bool>> filter);
        Task<IPagedResult<EmployeeProfile>> GetPagedEmployeesAsync(int pageSize, int pageNumber, string serviceName,
            decimal? maxServiceCost, int? minAverageRate);
    }
}
