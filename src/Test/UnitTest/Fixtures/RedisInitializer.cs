using StackExchange.Redis;

namespace UnitTest.Fixtures
{
    public class RedisInitializer : IAsyncLifetime
    {

        public IConnectionMultiplexer RedisConnection { get; private set; }
        public RedisInitializer()
        {
        }
        public async Task DisposeAsync()
        {
            RedisConnection.Dispose();
            await Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            RedisConnection = ConnectionMultiplexer
                .Connect("127.0.0.1:6379");
            await Task.CompletedTask;
        }
    }
}
