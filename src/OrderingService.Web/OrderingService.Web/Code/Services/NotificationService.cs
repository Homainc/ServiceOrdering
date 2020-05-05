using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OrderingService.Domain.Logic.Code.Interfaces;
using OrderingService.Web.Code.Interfaces;
using OrderingService.Web.Hubs;

namespace OrderingService.Web.Code.Services
{
    public class NotificationService : INotificationService
    {
        private const string ReceiveNoticeMethod = "ReceiveNotice";

        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IEmployeeService _employeeService;
        public NotificationService(IHubContext<NotificationHub> hubContext, IEmployeeService employeeService)
        {
            _hubContext = hubContext;
            _employeeService = employeeService;
        }

        public async Task NoticeByUserIdAsync(Guid usedId, string message) =>
            await _hubContext.Clients.User(usedId.ToString()).SendAsync(ReceiveNoticeMethod, message);

        public async Task NoticeByEmployeeIdAsync(Guid employeeId, string message)
        {
            var userId = await _employeeService.GetUserIdByEmployeeIdAsync(employeeId);
            await _hubContext.Clients.User(userId.ToString()).SendAsync(ReceiveNoticeMethod, message);
        }
    }
}
