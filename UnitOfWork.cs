using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IConfigurationR;
using TodoApp.IRepository;
using TodoApp.Repository;

namespace TodoApp
{
    public class UnitOfWork : IUnitOfWork
    {
        protected TodoDbContext _dbContext;
        protected ILogger _logger;
        public ITodoRepository TodoRepo { get; set; }
        public UnitOfWork(
            TodoDbContext dbContext,
            ILoggerFactory loggerFactory
            )
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger("logs");

            TodoRepo = new TodoRepository(_dbContext, _logger);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
