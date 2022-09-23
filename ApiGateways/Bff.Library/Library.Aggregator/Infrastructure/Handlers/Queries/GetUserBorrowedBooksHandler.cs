using System.Net;
using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Exceptions;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetUserBorrowedBooksHandler : IGetUserBorrowedBooksHandler
{
    private IBorrowClientService _borrowClientService;
    private IBookClientService _bookClientService;

    public GetUserBorrowedBooksHandler(IBorrowClientService borrowClientService, IBookClientService bookClientService)
    {
        _borrowClientService = borrowClientService;
        _bookClientService = bookClientService;
    }

    public async Task<IList<UserBorrowedBooksResponse>> Handle(UserBorrowedBooksRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _borrowClientService.GetBorrowedBooksByUserIdAsync(request.UserId);
        if (borrowedBooks == null || !borrowedBooks.Any())
        {
            throw new ResponseException(HttpStatusCode.BadRequest, "there is no book borrowed by this userId");
        }
        var ids = borrowedBooks.Select(x => x.BookId.ToString()).Aggregate((c, n) => c + "," + n);
        var books = await _bookClientService.GetBooksByIdsAsync(ids);
        return books.Join(borrowedBooks, x => x.Id, y => y.BookId,
            (x, y) => new UserBorrowedBooksResponse
            {
                Title = x.Title,
                BorrowedDate = y.BorrowedDate,
                ReturnDate = y.ReturnDate
            }).ToList();

    }
}