using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using OrderingService.Data.EF;
using OrderingService.Data.Models;
using OrderingService.Data.Interfaces;

namespace OrderingService.Data.Repositories {
    public class UserRepository : AbstractRepository<User>, IUserRepository 
    {
        public UserRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor) : base(db,
            httpContextAccessor)
        {
        }

        public override IQueryable<User> GetAll() => Db.Users.AsQueryable();
        public async Task<User> EagerSingleAsync(Expression<Func<User, bool>> filter){
            var user = await (
                from u in Db.Users
                join r in Db.Roles on u.RoleId equals r.Id
                join e in Db.EmployeeProfiles on u.Id equals e.UserId into eGrouping
                from e in eGrouping.DefaultIfEmpty() 
                join st in Db.ServiceTypes on e.ServiceTypeId equals st.Id into stGrouping
                from st in stGrouping.DefaultIfEmpty()
                select new User
                {
                    Email = u.Email,
                    EmployeeProfile = e != null ? new EmployeeProfile {
                        Id = e.Id,
                        ServiceType = st,
                        ServiceCost = e.ServiceCost,
                        Description = e.Description,
                        ServiceTypeId = e.ServiceTypeId
                    }: null,
                    FirstName = u.FirstName,
                    Id = u.Id,
                    ImageUrl = u.ImageUrl,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Role = r,
                    RoleId = r.Id,
                    HashedPassword = u.HashedPassword
                })
                .SingleOrDefaultAsync(filter, Token);
            return user;
        }

        public async Task<bool> AnyUserAsync(Expression<Func<User, bool>> filter) => 
            await Db.Users.AnyAsync(filter, Token);
        }
}