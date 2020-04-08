using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> EagerSingleAsync(Expression<Func<User, bool>> filter);
        Task<bool> AnyUserAsync(Expression<Func<User, bool>> filter);
    }
}