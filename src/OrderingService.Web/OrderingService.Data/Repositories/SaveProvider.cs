using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories
{
    public class SaveProvider: ISaveProvider
    {
        private readonly ApplicationContext _db;
        private readonly CancellationToken _token;

        public SaveProvider(ApplicationContext db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _token = httpContext.HttpContext.RequestAborted;
        }

        public async Task SaveAsync() => await _db.SaveChangesAsync(_token);
    }
}
