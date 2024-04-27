using Domain.Base;
using Hasin.CacheCore.Contracts;

namespace Hasin.CacheCore.Implementations
{
    public class LayersCache : ICache
    {
        private readonly ICache _localCache;
        private readonly ICache _remoteCache;

        public LayersCache(ICache localCache, ICache remoteCache)
        {
            _localCache = localCache;
            _remoteCache = remoteCache;
        }

        public TimeSpan DefaultET => throw new NotImplementedException();

        public async Task AddAsync<T>(T item) where T : BaseEntity
        {
            await _localCache.AddAsync<T>(item);
            await _remoteCache.AddAsync<T>(item);

        }

        public async Task<T> GetAsync<T>(int key) where T : BaseEntity
        {
            var result = await _remoteCache.GetAsync<T>(key);
            if (result == null)
            {
                result = await _localCache.GetAsync<T>(key);
                if (result != null)
                {
                    await _remoteCache.AddAsync(result);
                }
            }
            return result;
        }

        public async Task Remove<T>(int key) where T : BaseEntity
        {
            await _localCache.Remove<T>(key);
            await _remoteCache.Remove<T>(key);
        }
    }
}
