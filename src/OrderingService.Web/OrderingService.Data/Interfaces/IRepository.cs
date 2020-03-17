using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderingService.Data.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Create(T entity);
        bool Delete(T entity);
        T Update(T entity);
    }
}
