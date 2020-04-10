using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.EF;

namespace OrderingService.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }

    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext Db;
        protected readonly CancellationToken Token;
        protected AbstractRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor)
        {
            Db = db;
            Token = httpContextAccessor.HttpContext.RequestAborted;
        }

        public abstract IQueryable<T> GetAll();
        public virtual void Create(T entity) => Db.Entry(entity).State = EntityState.Added;
        public void Update(T entity) => Db.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => Db.Entry(entity).State = EntityState.Deleted;
    }
}
