using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;
using OrderingService.Data.Models;

namespace OrderingService.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task CreateAsync(T entity, CancellationToken token);
        void Delete(T entity);
        void Update(T entity);
    }

    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext _db;
        protected AbstractRepository(ApplicationContext db) => _db = db;
        public abstract IQueryable<T> GetAll();
        public async Task CreateAsync(T entity, CancellationToken token) => await _db.AddAsync(entity, token);
        public void Update(T entity) => _db.Update(entity);
        public void Delete(T entity) => _db.Remove(entity);
    }
}
