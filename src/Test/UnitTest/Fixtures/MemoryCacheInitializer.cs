using Microsoft.Extensions.Caching.Memory;

namespace UnitTest.Fixtures
{
    public class MemoryCacheInitializer : IAsyncLifetime
    {

        public MemoryCache Cache { get; private set; }

        public async Task DisposeAsync()
        {
            Cache.Dispose();
            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
            await Task.CompletedTask;
        }
    }
}
