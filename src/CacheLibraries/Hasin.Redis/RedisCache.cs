using Domain.Base;
using Hasin.CacheCore.Contracts;
using StackExchange.Redis;

namespace Hasin.Redis
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _database;
        IItemSerializer _itemSerializer;
        public TimeSpan DefaultET { get; }
        public RedisCache(IDatabase database, IItemSerializer itemSerializer, TimeSpan defaultET)
        {
            _database = database;
            _itemSerializer = itemSerializer;
            DefaultET = defaultET;
        }

        public async Task AddAsync<T>(T item) where T : BaseEntity
        {
            await _database.StringSetAsync(
                typeof(T).Name + "_" + item.Id.ToString(),
                _itemSerializer.Serialize(item),
                DefaultET,
                When.Always,
                CommandFlags.FireAndForget);
        }

        public async Task<T> GetAsync<T>(int key) where T : BaseEntity
        {
            var packedBytes = await _database.StringGetAsync(typeof(T).Name + "_" + key.ToString());
            if (!packedBytes.IsNull)
                return _itemSerializer.Deserialize<T>(packedBytes.ToString());

            return null;
        }

        public async Task Remove<T>(int key) where T : BaseEntity
        {
            await _database.KeyDeleteAsync(typeof(T).Name + "_" + key.ToString());
        }
    }
}
