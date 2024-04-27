using Hasin.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Hasin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
                
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _bookService.GetData(id);

            return Ok(data?.Value);
        }
    }
}
