using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Code.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<int> GetRoleIdByNameAsync(string roleName);
    }
}
