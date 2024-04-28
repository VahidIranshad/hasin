using FunctionalTests.Controllers.Common;

namespace FunctionalTests.Controllers.Entity
{
    public class BookController : BaseControllerTests
    {

        public BookController(CustomWebApplicationFactory<Program> factory) : base(factory)
        {

        }
        [Fact]
        public async Task Valid_GetData_ReturnsList()
        {
            var client = this.GetNewClientByAdminAuthorization();
            var response = await client.GetAsync($"/api/Book/1111");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<Book>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("OK", statusCode);
        }


        #region Private Methode

        #endregion
    }
}
