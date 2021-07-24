using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IConfigurationR;
using TodoApp.Models;

namespace TodoApp.Caching
{
    public class TodoCachingService : ITodoCachingService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ICacheBase _memoryCache;
        protected readonly IMemoryCache _inMemoryCache;

        public TodoCachingService(
            IUnitOfWork unitOfWork,
            ICacheBase memoryCache,
            IMemoryCache inMemoryCache
            )
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _inMemoryCache = inMemoryCache;
        }
        public IEnumerable<ItemData> GetAll()
        {
            try
            {
                return _memoryCache.GetOrRemove<List<ItemData>>("GET_TODO_LIST", TimeSpan.FromSeconds(300), () =>
                {
                    return _unitOfWork.TodoRepo.GetAll().Result.ToList();
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
