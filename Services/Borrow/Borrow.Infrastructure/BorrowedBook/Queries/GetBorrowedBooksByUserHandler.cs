using Borrow.Application.Queries;
using Borrow.Contracts.DTO;

namespace Borrow.Infrastructure.BorrowedBook.Queries;

public class GetBorrowedBooksByUserHandler : IGetBorrowedBooksByUserHandler
{
    private IBorrowRepository _repository;

    public GetBorrowedBooksByUserHandler(IBorrowRepository repository)
    {
        _repository = repository;
    }
    public async Task<IList<GetBorrowedBooksByUserResponse>> Handle(GetBorrowedBooksByUserRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _repository.GetBorrowedBooksByUserIdAsync(request.UserId);
        return borrowedBooks.Select(x => new GetBorrowedBooksByUserResponse
        {
            BookId = x.BookId,
            UserId = x.UserId,
            BorrowedDate = x.BorrowedDate,
            ReturnDate = x.ReturnDate
        }).ToList();
    }
}