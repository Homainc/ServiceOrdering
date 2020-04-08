using System.Threading.Tasks;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IServiceOrderRepository : IRepository<ServiceOrder>
    {
        Task<ServiceOrder> SingleByIdAsync(int id);
        Task<bool> AnyOrderById(int id);
    }
}
