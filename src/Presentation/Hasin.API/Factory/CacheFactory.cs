using Domain.Base;
using Hasin.CacheCore.Contracts;
using Hasin.CacheCore.Implementations;
using Hasin.InMemory;
using Hasin.Redis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Hasin.API.Factory
{
    public class CacheFactory : ICacheFactory
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IItemSerializer _itemSerializer;
        private readonly AppSetting _appSetting;
        private readonly MemoryCache _cache;


        public ICache Cache { get; private set; }

        public CacheFactory(IConnectionMultiplexer redisConnection, IOptions<AppSetting> appSetting)
        {
            _redisConnection = redisConnection;
            _itemSerializer = new JsonItemSerializer();
            _appSetting = appSetting.Value;
            _cache = new MemoryCache(new MemoryCacheOptions());
            Cache = CreateLayersCache();
        }
        private ICache CreateLayersCache()
        {
            return new LayersCache(
                new RedisCache(_redisConnection.GetDatabase(), _itemSerializer, _appSetting.RedisDefaultET),
                new InMemoryCache(_appSetting.InMemoryCacheET, _cache)
            );
        }

    }
}
