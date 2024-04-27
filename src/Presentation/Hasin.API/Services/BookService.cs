using Domain.Base;
using Domain.Entity;
using Domain.Exception;
using Hasin.API.Factory;
using Microsoft.Extensions.Options;

namespace Hasin.API.Services
{
    public class BookService
    {
        private readonly CacheFactory _cacheFactory;
        private readonly AppSetting _appSetting;

        public BookService(CacheFactory cacheFactory, IOptions<AppSetting> appSetting)
        {
            _cacheFactory = cacheFactory;
            _appSetting = appSetting.Value;
        }

        public async Task<Book> GetData(int id)
        {
            var result = await _cacheFactory.Cache.GetAsync<Book>(id);
            if (result == null)
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"{_appSetting.HasinServerUrl}{id}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var bookJson = await response.Content.ReadAsStringAsync();
                    if (bookJson != null && bookJson.ToLower() != "null")
                    {
                        var book = new Book { Id = id , Value = bookJson};
                        await _cacheFactory.Cache.AddAsync<Book>(book);
                        result = book;
                    }
                    else
                    {
                        throw new NotFoundException(typeof(Book).Name, id);
                    }
                }
            }
            return result;
        }
    }
}
