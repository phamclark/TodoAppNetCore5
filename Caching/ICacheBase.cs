using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Caching
{
    public interface ICacheBase
    {
        T Get<T>(string key);
        void Add<T>(string key, T cacheData);
        void Remove(string key);
        T GetOrRemove<T>(string key, TimeSpan timeExpiredCache, Func<T> cacheData);
    }
}
