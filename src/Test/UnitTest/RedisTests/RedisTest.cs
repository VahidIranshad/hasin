using Domain.Entity;
using Hasin.CacheCore.Implementations;
using Hasin.Redis;
using UnitTest.Fixtures;

namespace UnitTest.RedisTest
{
    public class RedisTest : IClassFixture<RedisInitializer>
    {
        private readonly RedisCache _redisCache;

        private readonly TimeSpan _expireTime;
        public RedisTest(RedisInitializer redisInitializer)
        {
            _expireTime = new TimeSpan(0, 0, 5);
            _redisCache = new RedisCache(redisInitializer.RedisConnection.GetDatabase(), new JsonItemSerializer(), _expireTime);
        }

        [Fact]
        public async Task Invalid_GetAsync_Id_Not_Exist()
        {
            var id = 0;

            var result = await _redisCache.GetAsync<Book>(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task Valid_SetAsync_GetAsync_RemoveAsync()
        {

            var book = new Book() { Id = -1, Value = "Test" };
            await _redisCache.AddAsync<Book>(book);
            var result = await _redisCache.GetAsync<Book>(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Value, result.Value);

            await _redisCache.Remove<Book>(book.Id);
            result = await _redisCache.GetAsync<Book>(book.Id);

            Assert.Null(result);
        }


        [Fact]
        public async Task Valid_Get_AfterEpiration()
        {

            var book = new Book() { Id = -1, Value = "Test" };
            await _redisCache.AddAsync<Book>(book);

            await Task.Delay(_expireTime);

           var result = await _redisCache.GetAsync<Book>(book.Id);

            Assert.Null(result);
        }
    }
}
