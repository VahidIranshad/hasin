using EventBus.Events;
using Hasin.API.Services;
using MassTransit;

namespace Hasin.API.EventBusConsumer
{
    public class EventBusBookConsumer : IConsumer<BookIdEvent>
    {

        private readonly BookService _bookService;
        public EventBusBookConsumer(BookService bookService)
        {
            _bookService = bookService;
        }


        public async Task Consume(ConsumeContext<BookIdEvent> context)
        {
            await _bookService.RemoveData(context.Message.BookId);
        }

    }
}
