using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetMostBorrowedBooksHandler : IGetMostBorrowedBooksHandler
{
    private IBorrowClientService _borrowClientService;
    private IBookClientService _bookClientService;

    public GetMostBorrowedBooksHandler(IBorrowClientService borrowClientService, IBookClientService bookClientService)
    {
        _borrowClientService = borrowClientService;
        _bookClientService = bookClientService;
    }

    public async Task<IList<MostBorrowedBooksResponse>> Handle(MostBorrowedBooksRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _borrowClientService.GetMostBorrowedBooksAsync();
        var ids = borrowedBooks.Select(x => x.BookId.ToString()).Aggregate((c, n) => c + "," + n);
        var books = await _bookClientService.GetBooksByIdsAsync(ids);
        return books.Join(borrowedBooks, x => x.Id, y => y.BookId,
            (x, y) => new MostBorrowedBooksResponse
            {
                Title = x.Title,
                BookId = x.Id,
                BorrowedCount = y.BorrowedCount,
                TotalCopies = x.TotalCopies
            }).OrderByDescending(x => x.BorrowedCount).ToList();
    }
}