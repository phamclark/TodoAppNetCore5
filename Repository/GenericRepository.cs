using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IRepository;

namespace TodoApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected TodoDbContext _dbContext;
        protected ILogger _logger;
        public GenericRepository(
            ILogger logger,
            TodoDbContext  dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbSet = _dbContext.Set<T>();
        }
        public virtual async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int ID)
        {
            return await _dbSet.FindAsync(ID);
        }

        public virtual Task<bool> Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
