using Microsoft.Extensions.Caching.Memory;

namespace UnitTest.Fixtures
{
    public class MemoryCacheInitializer : IAsyncLifetime
    {

        public MemoryCache _cache { get; private set; }

        public async Task DisposeAsync()
        {
            _cache.Dispose();
            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            await Task.CompletedTask;
        }
    }
}
