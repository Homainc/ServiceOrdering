using System.Collections.Generic;
using System.Data.Common;
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

        public async Task<IEnumerable<ServiceType>> GetAllOrderedByProfilesCount()
        {
            var query =
                from e in Db.EmployeeProfiles
                join st in Db.ServiceTypes on e.ServiceType.Id equals st.Id
                group e by new { st.Id, st.Name } into g
                select new
                {
                    g.Key.Id,
                    g.Key.Name,
                    ProfilesCount = g.Count()
                };

            var serviceTypes = await (
                from g in query
                orderby g.ProfilesCount
                select new ServiceType
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToListAsync(Token);
            return serviceTypes;
        }
    }
}
