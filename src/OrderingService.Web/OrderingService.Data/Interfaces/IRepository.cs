using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingService.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        void Delete(int id);
        void Update(T entity);
    }
}
