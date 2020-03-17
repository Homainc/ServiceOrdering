using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Interfaces;
using OrderingService.Data.Models;

namespace OrderingService.Data.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly ApplicationContext _db;
        public UserProfileRepository(ApplicationContext appContext)
        {
            _db = appContext;
        }

        public IQueryable<UserProfile> GetAll() => _db.UserProfiles.AsQueryable();

        public void Create(UserProfile entity) => _db.Add(entity);

        public void Update(UserProfile entity) => _db.Entry(entity).State = EntityState.Modified;

        public void Delete(int id)
        {
            var userProfile = _db.UserProfiles.Find(id);
            if (userProfile != null)
                _db.UserProfiles.Remove(userProfile);
        }
    }
}
