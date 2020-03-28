using System.Threading;
using System.Threading.Tasks;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories
{
    public class SaveProvider: ISaveProvider
    {
        private readonly ApplicationContext _db;
        public SaveProvider(ApplicationContext db) => _db = db;
        public async Task SaveAsync(CancellationToken token) => await _db.SaveChangesAsync(token);
    }
}
