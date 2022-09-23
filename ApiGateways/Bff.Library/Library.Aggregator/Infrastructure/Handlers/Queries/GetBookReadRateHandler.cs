using System.Net;
using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Exceptions;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetBookReadRateHandler : IGetBookReadRateHandler
{
    private IBorrowClientService _borrowClientService;
    private IBookClientService _bookClientService;

    public GetBookReadRateHandler(IBorrowClientService borrowClientService, IBookClientService bookClientService)
    {
        _borrowClientService = borrowClientService;
        _bookClientService = bookClientService;
    }

    public async Task<BookReadRateResponse> Handle(BookReadRateRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _borrowClientService.GetBookWithBorrowedDaysAsync(request.BookId);
        if (borrowedBooks == null || !borrowedBooks.Any())
        {
            throw new ResponseException(HttpStatusCode.BadRequest, "there is no book with this given id");
        }
        var books = await _bookClientService.GetBooksByIdsAsync(request.BookId.ToString());
        var book = books.FirstOrDefault();
        var daysCount = borrowedBooks.Average(x => book.Pages / x.BorrowedDaysCount);
        return new BookReadRateResponse
        {
            Title = book.Title, Pages = book.Pages, PagesPerDay = (int)daysCount, BorrowedCount = borrowedBooks.Count
        };
    }
}