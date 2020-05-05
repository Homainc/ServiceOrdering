using Microsoft.AspNetCore.SignalR;

namespace OrderingService.Web.Code.Services
{
    public class NameUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection) =>
            connection.User?.Identity?.Name;
    }
}
