using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceTypeRepository : AbstractRepository<ServiceType> 
    {
        public ServiceTypeRepository(ApplicationContext db):base(db) { }

        public override IQueryable<ServiceType> GetAll() => _db.ServiceTypes.AsNoTracking().AsQueryable();
    }
}
