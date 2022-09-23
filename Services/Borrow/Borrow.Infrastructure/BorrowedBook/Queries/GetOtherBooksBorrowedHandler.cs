using Borrow.Application.Persistence;
using Borrow.Application.Queries;
using Borrow.Contracts.DTO;
using Microsoft.EntityFrameworkCore;

namespace Borrow.Infrastructure.BorrowedBook.Queries;

public class GetOtherBooksBorrowedHandler: IGetOtherBooksBorrowedHandler
{
    private IBorrowRepository _repository;

    public GetOtherBooksBorrowedHandler(IBorrowRepository repository)
    {
        _repository = repository;
    }
    public async Task<IList<GetOtherBooksBorrowedResponse>> Handle(GetOtherBooksBorrowedRequest request, CancellationToken cancellationToken)
    {
        var otherBooks = await _repository.GetAllBorrowed(request.BookId)
            .Select(x => x.UserId)
            .Join(_repository.GetAllBorrowed(), x => x, y => y.UserId, (x, y) => y)
            .Where(x => x.BookId != request.BookId)
            .GroupBy(x => x.BookId, (g, l) => new { BookId = g, BorrowedCount = l.Count() })
            .OrderByDescending(x => x.BorrowedCount)
            .ToListAsync();
        return otherBooks.Select(x => new GetOtherBooksBorrowedResponse
            { BookId = x.BookId, BorrowedCount = x.BorrowedCount }).ToList();
    }
}