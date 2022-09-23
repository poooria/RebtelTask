using Borrow.Contracts.DTO;
using Borrow.Contracts.Services;

namespace Library.Aggregator.Services;

public class BorrowClientService : IBorrowClientService
{
    private readonly IGrpcBorrowService _borrowClient;

    public BorrowClientService(IGrpcBorrowService borrowClient)
    {
        _borrowClient = borrowClient;
    }

    public async Task<List<GetMostBorrowedBooksResponse>> GetMostBorrowedBooksAsync()
    {
        return await _borrowClient.GetMostBorrowedBooksAsync(new GetMostBorrowedBooksRequest());
    }

    public async Task<List<GetMostBorrowersResponse>> GetMostBorrowersAsync(GetMostBorrowersRequest request)
    {
        return await _borrowClient.GetMostBorrowersAsync(new GetMostBorrowersRequest { StartDate = request.StartDate, EndDate = request.EndDate });
    }

    public async Task<List<GetOtherBooksBorrowedResponse>> GetOtherBooksAlsoBorrowedAsync(int bookId)
    {
        return await _borrowClient.GetOtherBooksAlsoBorrowedAsync(new GetOtherBooksBorrowedRequest { BookId = bookId });
    }

    public async Task<List<GetBooksWithBorrowedDaysResponse>> GetBookWithBorrowedDaysAsync(int bookId)
    {
        return await _borrowClient.GetBookWithBorrowedDaysAsync(new GetBooksWithBorrowedDaysRequest { BookId = bookId });
    }

    public async Task<List<GetBorrowedBooksByUserResponse>> GetBorrowedBooksByUserIdAsync(int userId)
    {
        return await _borrowClient.GetBorrowedBooksByUserIdAsync(new GetBorrowedBooksByUserRequest { UserId = userId });
    }
}