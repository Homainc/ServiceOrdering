using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceTypeRepository : AbstractRepository<ServiceType> 
    {
        public ServiceTypeRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<ServiceType> GetAll() => Db.ServiceTypes.AsQueryable();
    }
}
