using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetBookAvailableHandler: IGetBookAvailableHandler
{
    private IBookClientService _bookClientService;

    public GetBookAvailableHandler(IBookClientService bookClientService)
    {
        _bookClientService = bookClientService;
    }

    public async Task<BookAvailableBorrowResponse> Handle(BookAvailableBorrowRequest request, CancellationToken cancellationToken)
    {
        var book = await _bookClientService.GetBookAvailableBorrowAsync(request.BookId);
        return new BookAvailableBorrowResponse() { Available = book.Available, Borrowed = book.Borrowed };
    }
}