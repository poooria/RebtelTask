using Borrow.Contracts.DTO;
using Library.Aggregator.Abstractions.Queries;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Services;

namespace Library.Aggregator.Infrastructure.Handlers.Queries;

public class GetMostBorrowersHandler : IGetMostBorrowersHandler
{
    private IBorrowClientService _borrowClientService;
    private IUserClientService _userClientService;

    public GetMostBorrowersHandler(IBorrowClientService borrowClientService, IUserClientService userClientService)
    {
        _borrowClientService = borrowClientService;
        _userClientService = userClientService;
    }

    public async Task<IList<MostBorrowersResponse>> Handle(MostBorrowersRequest request, CancellationToken cancellationToken)
    {
        var borrowedBooks = await _borrowClientService.GetMostBorrowersAsync(new GetMostBorrowersRequest
        { StartDate = request.StartDate, EndDate = request.EndDate });
        var ids = borrowedBooks.Select(x => x.UserId.ToString()).Aggregate((c, n) => c + "," + n);
        var users = await _userClientService.GetUsersByIdsAsync(ids);
        return users.Join(borrowedBooks, x => x.Id, y => y.UserId,
            (x, y) => new MostBorrowersResponse
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                UniqId = x.UniqId,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Address = x.Address,
                BorrowedCount = y.BorrowedCount
            }).OrderByDescending(x => x.BorrowedCount).ToList();
    }
}