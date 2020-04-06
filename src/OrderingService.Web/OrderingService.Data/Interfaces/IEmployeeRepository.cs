using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<EmployeeProfile>
    {
        Task<bool> AnyEmployeeAsync(Expression<Func<EmployeeProfile, bool>> filter);
        Task<EmployeeProfile> EagerSingleOrDefaultAsync(Expression<Func<EmployeeProfile, bool>> filter);
    }
}
