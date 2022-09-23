using Borrow.Contracts.DTO;

namespace Library.Aggregator.Services;

public interface IBorrowClientService
{
    Task<List<GetMostBorrowedBooksResponse>> GetMostBorrowedBooksAsync();
    Task<List<GetMostBorrowersResponse>> GetMostBorrowersAsync(GetMostBorrowersRequest request);
    Task<List<GetOtherBooksBorrowedResponse>> GetOtherBooksAlsoBorrowedAsync(int bookId);
    Task<List<GetBooksWithBorrowedDaysResponse>> GetBookWithBorrowedDaysAsync(int bookId);
    Task<List<GetBorrowedBooksByUserResponse>> GetBorrowedBooksByUserIdAsync(int userId);
}