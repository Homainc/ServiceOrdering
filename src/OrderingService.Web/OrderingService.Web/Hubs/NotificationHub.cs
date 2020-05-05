using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OrderingService.Web.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    { }
}
