using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceTypeRepository : AbstractRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<ServiceType> GetAll() => Db.ServiceTypes.AsQueryable();

        public async Task<ServiceType> GetByNameOrCreateNewAsync(string name)
        {
            var serviceType =
                await Db.ServiceTypes.SingleOrDefaultAsync(
                    x => x.Name.ToLower() == name.ToLower(), Token);
            if (serviceType != null) return serviceType;

            serviceType = new ServiceType {Name = name};
            Create(serviceType);
            
            return serviceType;
        }
    }
}
