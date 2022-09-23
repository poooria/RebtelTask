using Borrow.Application.Queries;
using Borrow.Contracts.DTO;

namespace Borrow.Infrastructure.BorrowedBook.Queries;

public class GetMostBorrowedBooksHandler:IGetMostBorrowedBooksHandler
{
    private IBorrowRepository _repository;

    public GetMostBorrowedBooksHandler(IBorrowRepository repository)
    {
        _repository = repository;
    }
    public async Task<IList<GetMostBorrowedBooksResponse>> Handle(GetMostBorrowedBooksRequest request, CancellationToken cancellationToken)
    {
        var orderedBooksIds = await _repository.GetAllBorrowed()
            .GroupBy(x => x.BookId, (g, l) => new { BookId = g, BorrowedCount = l.Count() })
            .OrderByDescending(x => x.BorrowedCount)
            .Take(10)
            .ToListAsync();
        return orderedBooksIds.Select(x => new GetMostBorrowedBooksResponse { BookId = x.BookId, BorrowedCount = x.BorrowedCount })
            .ToList();
    }
}