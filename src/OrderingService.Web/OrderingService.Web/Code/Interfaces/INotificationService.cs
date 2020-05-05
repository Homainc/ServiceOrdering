using System;
using System.Threading.Tasks;

namespace OrderingService.Web.Code.Interfaces
{
    public interface INotificationService
    {
        Task NoticeByUserIdAsync(Guid usedId, string message);
        Task NoticeByEmployeeIdAsync(Guid employeeId, string message);
    }
}
