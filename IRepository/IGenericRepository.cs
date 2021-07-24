using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.IRepository
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int ID);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<bool> Remove(T entity);
    }
}
