using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Caching
{
    public interface ITodoCachingService
    {
        IEnumerable<ItemData> GetAll();
    }
}
