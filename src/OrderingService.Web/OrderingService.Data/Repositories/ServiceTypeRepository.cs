using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Code.Abstractions;
using OrderingService.Data.Code.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class ServiceTypeRepository : AbstractRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

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
                from st in Db.ServiceTypes
                join e in Db.EmployeeProfiles on st.Id equals e.ServiceTypeId into eGrouping
                from e in eGrouping.DefaultIfEmpty()
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

        public async Task<ServiceType> GetByIdOrDefaultAsync(int id) =>
            await Db.ServiceTypes.SingleOrDefaultAsync(x => x.Id == id, Token);

        public async Task<bool> AnyServiceAsync(Expression<Func<ServiceType, bool>> filter)
            => await Db.ServiceTypes.AnyAsync(filter, Token);
    }
}
