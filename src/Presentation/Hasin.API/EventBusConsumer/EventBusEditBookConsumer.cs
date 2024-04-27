using Domain.Entity;
using EventBus.Events;
using Hasin.API.Services;
using MassTransit;

namespace Hasin.API.EventBusConsumer
{
    public class EventBusEditBookConsumer : IConsumer<BookEvent>
    {

        private readonly BookService _bookService;
        public EventBusEditBookConsumer(BookService bookService, ILogger<EventBusEditBookConsumer> logger)
        {

            _bookService = bookService;
            logger.Log(LogLevel.Information, "EventBusEditBookConsumer");
        }


        public async Task Consume(ConsumeContext<BookEvent> context)
        {
            var book = new Book { Id = context.Message.BookId, Value = context.Message.BookValue };
            await _bookService.AddData(book);
        }

    }
}
