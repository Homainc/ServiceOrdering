using System.Linq;

namespace OrderingService.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        void Delete(T id);
        void Update(T entity);
    }
}
