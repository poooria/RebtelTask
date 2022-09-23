using Borrow.Application.Queries;
using Borrow.Contracts.DTO;

namespace Borrow.Infrastructure.BorrowedBook.Queries;

public class GetBooksWithBorrowedDaysHandler: IGetBooksWithBorrowedDaysHandler
{
    private IBorrowRepository _repository;

    public GetBooksWithBorrowedDaysHandler(IBorrowRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<GetBooksWithBorrowedDaysResponse>> Handle(GetBooksWithBorrowedDaysRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _repository.GetAllReturnedBooksByBookIdAsync(request.BookId);
        return borrowedBooks.Select(x => new GetBooksWithBorrowedDaysResponse
        { BookId = x.BookId, UserId = x.UserId, BorrowedDaysCount = x.ReturnDate.Value.Subtract(x.BorrowedDate).TotalDays }).ToList();
    }
}