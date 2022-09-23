using Borrow.Application.Queries;
using Borrow.Contracts.DTO;

namespace Borrow.Infrastructure.BorrowedBook.Queries;

public class GetMostBorrowersHandler : IGetMostBorrowersHandler
{
    private IBorrowRepository _repository;

    public GetMostBorrowersHandler(IBorrowRepository repository)
    {
        _repository = repository;
    }
    public async Task<IList<GetMostBorrowersResponse>> Handle(GetMostBorrowersRequest request, CancellationToken cancellationToken)
    {
        var borrowers= await _repository.GetAllBorrowed().WhereInTimeFrame(request.StartDate, request.EndDate)
            .GroupBy(x => x.UserId, (g, l) => new { UserId = g, BorrowedCount = l.Count() })
            .OrderByDescending(x => x.BorrowedCount)
            .Take(10)
            .Select(x => new GetMostBorrowersResponse
            {
                UserId = x.UserId,
                BorrowedCount = x.BorrowedCount
            })
            .ToListAsync();

        return borrowers;
    }
}