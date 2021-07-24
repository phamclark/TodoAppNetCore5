using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IRepository;

namespace TodoApp.IConfigurationR
{
    public interface IUnitOfWork
    {
        ITodoRepository TodoRepo { get; }
        Task CompleteAsync();
    }
}
