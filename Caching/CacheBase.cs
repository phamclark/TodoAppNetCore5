using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Caching
{
    public class CacheBase : ICacheBase
    {
        protected readonly IMemoryCache _memoryCache;
        public CacheBase(
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Add<T>(string key, T cacheData)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public T GetOrRemove<T>(string key, TimeSpan timeExpiredCache, Func<T> cacheData)
        {
            return _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = timeExpiredCache;
                return cacheData.Invoke();
            });
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
