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
    public class BookPublicherController: ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BookPublicherController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var model = new BookIdEvent() { BookId = id };

            await _publishEndpoint.Publish(model);
            return Accepted();
        }

    }
}
