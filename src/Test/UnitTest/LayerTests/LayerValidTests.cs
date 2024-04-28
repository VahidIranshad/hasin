using Domain.Entity;
using Hasin.CacheCore.Contracts;
using Hasin.CacheCore.Implementations;
using Hasin.InMemory;
using Hasin.Redis;
using UnitTest.Fixtures;

namespace UnitTest.LayerTests
{
    public class LayerValidTests : IClassFixture<RedisInitializer>, IClassFixture<MemoryCacheInitializer>
    {
        public LayersCache _layerCache;
        private readonly ICache _memoryCache;
        private readonly RedisCache _redisCache;

        private readonly TimeSpan _redisExpireTime;
        private readonly TimeSpan _memoryExpireTime;

        public LayerValidTests(RedisInitializer redisInitializer, MemoryCacheInitializer memoryCacheInitializer)
        {

            _redisExpireTime = TimeSpan.FromSeconds(5);
            _memoryExpireTime = TimeSpan.FromSeconds(2);

            _redisCache = new RedisCache(redisInitializer.RedisConnection.GetDatabase(), new JsonItemSerializer(), _redisExpireTime);
            _memoryCache = new InMemoryCache(_memoryExpireTime, memoryCacheInitializer.Cache);

            _layerCache = new LayersCache(
             _redisCache,
               _memoryCache);
        }

        [Fact]
        public async Task Invalid_GetAsync_Id_Not_Exist()
        {
            var id = 0;

            var result = await _layerCache.GetAsync<Book>(id);
            Assert.Null(result);

            var redisResult = await _redisCache.GetAsync<Book>(id);
            Assert.Null(redisResult);

            var memoryResult = await _memoryCache.GetAsync<Book>(id);
            Assert.Null(memoryResult);
        }

        [Fact]
        public async Task Valid_SetAsync_GetAsync_RemoveAsync()
        {

            var book = new Book() { Id = -2, Value = "Test" };
            await _layerCache.AddAsync<Book>(book);

            var result = await _layerCache.GetAsync<Book>(book.Id);
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Value, result.Value);

            var redisResult = await _redisCache.GetAsync<Book>(book.Id);
            Assert.NotNull(redisResult);
            Assert.Equal(book.Id, redisResult.Id);
            Assert.Equal(book.Value, redisResult.Value);

            var memoryResult = await _memoryCache.GetAsync<Book>(book.Id);
            Assert.NotNull(memoryResult);
            Assert.Equal(book.Id, memoryResult.Id);
            Assert.Equal(book.Value, memoryResult.Value);

            await _layerCache.Remove<Book>(book.Id);

            result = await _layerCache.GetAsync<Book>(book.Id);
            Assert.Null(result);

            redisResult = await _redisCache.GetAsync<Book>(book.Id);
            Assert.Null(redisResult);

            memoryResult = await _memoryCache.GetAsync<Book>(book.Id);
            Assert.Null(memoryResult);
        }


        [Fact]
        public async Task InValid_Get_AfterEpirationBothCaches()
        {

            var book = new Book() { Id = -2, Value = "Test" };
            await _layerCache.AddAsync<Book>(book);

            await Task.Delay((_redisExpireTime > _memoryExpireTime ? _redisExpireTime : _memoryExpireTime));

            var result = await _layerCache.GetAsync<Book>(book.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task Valid_Get_AfterEpirationOfFirsCache()
        {

            var book = new Book() { Id = -2, Value = "Test" };
            await _layerCache.AddAsync<Book>(book);

            await Task.Delay(_redisExpireTime < _memoryExpireTime ? _redisExpireTime : _memoryExpireTime);

            var result = await _layerCache.GetAsync<Book>(book.Id);


            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Value, result.Value);

            await _layerCache.Remove<Book>(book.Id);
            result = await _layerCache.GetAsync<Book>(book.Id);
            Assert.Null(result);


        }
    }

}

