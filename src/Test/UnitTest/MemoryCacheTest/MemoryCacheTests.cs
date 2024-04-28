using Domain.Entity;
using Hasin.CacheCore.Contracts;
using Hasin.InMemory;
using UnitTest.Fixtures;

namespace UnitTest.MemoryCacheTest
{
    public class MemoryCacheTests : IClassFixture<MemoryCacheInitializer>
    {
        private readonly ICache _memoryCache;

        private readonly TimeSpan _expireTime;
        public MemoryCacheTests(MemoryCacheInitializer memoryCacheInitializer)
        {
            _expireTime = new TimeSpan(0, 0, 5);
            _memoryCache = new InMemoryCache(_expireTime, memoryCacheInitializer._cache);
        }

        [Fact]
        public async Task Invalid_GetAsync_Id_Not_Exist()
        {
            var id = 0;

            var result = await _memoryCache.GetAsync<Book>(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task Valid_SetAsync_GetAsync_RemoveAsync()
        {

            var book = new Book() { Id = -1, Value = "Test" };
            await _memoryCache.AddAsync<Book>(book);
            var result = await _memoryCache.GetAsync<Book>(book.Id);

            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Value, result.Value);

            await _memoryCache.Remove<Book>(book.Id);
            result = await _memoryCache.GetAsync<Book>(book.Id);

            Assert.Null(result);
        }


        [Fact]
        public async Task Valid_Get_AfterEpiration()
        {

            var book = new Book() { Id = -1, Value = "Test" };
            await _memoryCache.AddAsync<Book>(book);

            await Task.Delay(_expireTime);

            var result = await _memoryCache.GetAsync<Book>(book.Id);

            Assert.Null(result);
        }
    }
}