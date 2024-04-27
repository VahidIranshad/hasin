using Hasin.CacheCore.Contracts;
using Newtonsoft.Json;

namespace Hasin.CacheCore.Implementations
{
    public class JsonItemSerializer : IItemSerializer
    {
        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        public string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
