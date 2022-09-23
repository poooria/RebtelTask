using System.Net;
using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Exceptions;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetOtherBooksAlsoBorrowedHandler : IGetOtherBooksAlsoBorrowedHandler
{
    private IBorrowClientService _borrowClientService;
    private IBookClientService _bookClientService;

    public GetOtherBooksAlsoBorrowedHandler(IBorrowClientService borrowClientService, IBookClientService bookClientService)
    {
        _borrowClientService = borrowClientService;
        _bookClientService = bookClientService;
    }

    public async Task<IList<OtherBooksAlsoBorrowedResponse>> Handle(OtherBooksAlsoBorrowedRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _borrowClientService.GetOtherBooksAlsoBorrowedAsync(request.BookId);
        var ids = "";
        if (borrowedBooks.Any())
        {
            ids = borrowedBooks.Select(x => x.BookId.ToString()).Aggregate((c, n) => c + "," + n);
        }
        else
        {
            throw new ResponseException(HttpStatusCode.BadRequest, "Thers is no book with this given id");
        }
        var books = await _bookClientService.GetBooksByIdsAsync(ids);
        return books.Join(borrowedBooks, x => x.Id, y => y.BookId,
            (x, y) => new OtherBooksAlsoBorrowedResponse
            {
                BookId = x.Id,
                Title = x.Title,
                BorrowedCount = y.BorrowedCount
            }).OrderByDescending(x => x.BorrowedCount).ToList();
    }
}