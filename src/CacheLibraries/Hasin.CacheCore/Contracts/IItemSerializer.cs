namespace Hasin.CacheCore.Contracts
{
    public interface IItemSerializer
    {
        string Serialize<T>(T item);

        T Deserialize<T>(string str);
    }
}
