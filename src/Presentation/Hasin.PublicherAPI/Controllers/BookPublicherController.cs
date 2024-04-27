using MassTransit.Mediator;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using EventBus.Events;

namespace Hasin.PublicherAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class BookPublicherController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BookPublicherController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var model = new BookIdEvent() { BookId = id };

            await _publishEndpoint.Publish(model);
            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] Book model)
        {
            var bookEvent = new BookEvent() { BookId = model.Id, BookValue = model.Value };

            await _publishEndpoint.Publish(bookEvent);
            return Accepted();
        }

    }

    public class Book
    {
        public int Id { get; set; }
        public required string Value { get; set; }
    }
}
