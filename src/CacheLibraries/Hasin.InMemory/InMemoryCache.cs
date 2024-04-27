using Domain.Base;
using Hasin.CacheCore.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Hasin.InMemory
{
    public class InMemoryCache : ICache
    {
        private readonly MemoryCache _cache;
        public TimeSpan DefaultET { get; }
        public InMemoryCache(TimeSpan defaultET, MemoryCache cache)
        {
            DefaultET = defaultET;
            if (cache == null)
                _cache = new MemoryCache(new MemoryCacheOptions ());
            else
                _cache = cache;
        }

        public Task AddAsync<T>(T item) where T : BaseEntity
        {
            using (var entry = _cache.CreateEntry(item.Id))
            {
                entry.AbsoluteExpirationRelativeToNow = DefaultET;
                entry.SetValue(item);
            }
            return Task.CompletedTask;
        }

        public Task<T> GetAsync<T>(int key) where T : BaseEntity
        {
            T result;

            _cache.TryGetValue(key, out result);

            return Task.FromResult(result);
        }


        public async Task Remove<T>(int key)where T : BaseEntity
        {
            var val = await GetAsync<T>(key);
            if (val != null)
            {
                _cache.Remove(key);
            }
        }
    }
}
