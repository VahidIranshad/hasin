using EventBus.Events;
using Hasin.API.Services;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Hasin.API.EventBusConsumer
{
    public class EventBusRemoveBookConsumer : IConsumer<BookIdEvent>
    {

        private readonly BookService _bookService;
        public EventBusRemoveBookConsumer(BookService bookService, ILogger<EventBusRemoveBookConsumer> logger)
        {
            _bookService = bookService;
            logger.Log(LogLevel.Information, "EventBusRemoveBookConsumer");
        }


        public async Task Consume(ConsumeContext<BookIdEvent> context)
        {
            await _bookService.RemoveData(context.Message.BookId);
        }

    }
}
