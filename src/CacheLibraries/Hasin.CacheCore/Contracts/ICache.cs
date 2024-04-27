using Domain.Base;

namespace Hasin.CacheCore.Contracts
{
    public interface ICache
    {
        Task AddAsync<T>(T item) where T : BaseEntity;

        Task<T> GetAsync<T>(int key) where T : BaseEntity;

        Task Remove<T>(int key) where T : BaseEntity;

        TimeSpan DefaultET { get; }
    }
}
