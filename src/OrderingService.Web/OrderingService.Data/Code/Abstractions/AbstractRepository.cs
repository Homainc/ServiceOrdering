using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderingService.Data.Code.Interfaces;

namespace OrderingService.Data.Code.Abstractions
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationContext Db;
        protected readonly CancellationToken Token;
        protected AbstractRepository(ApplicationContext db, IHttpContextAccessor httpContextAccessor)
        {
            Db = db;
            Token = httpContextAccessor.HttpContext.RequestAborted;
        }

        public virtual void Create(T entity) => Db.Entry(entity).State = EntityState.Added;
        public void Update(T entity) => Db.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => Db.Entry(entity).State = EntityState.Deleted;
    }
}
