using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IRepository;
using TodoApp.Models;

namespace TodoApp.Repository
{
    public class TodoRepository : GenericRepository<ItemData>, ITodoRepository
    {
        public TodoRepository(
            TodoDbContext dbContext,
            ILogger logger) : base(logger, dbContext)
        {

        }

        public override async Task<IEnumerable<ItemData>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAll method error", typeof(TodoRepository));
                return new List<ItemData>();
            }
            
        }

        public override async Task<ItemData> Update(ItemData entity)
        {
            try
            {
                var existingTodo = await _dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();

                if (existingTodo == null)
                    return await Add(entity);

                existingTodo.Done = !existingTodo.Done;
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(TodoRepository));
                return new ItemData();
            }
        }

        public override async Task<bool> Remove(ItemData entity)
        {
            try
            {
                var exist = await _dbSet.Where(x => x.ID == entity.ID).FirstOrDefaultAsync();

                if (exist == null)
                    return false;

                _dbSet.Remove(exist);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Remove method error", typeof(TodoRepository));
                return false;
            }
        }

        public override async Task<ItemData> Add(ItemData entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(TodoRepository));
                return new ItemData();
            }
        }
    }
}
