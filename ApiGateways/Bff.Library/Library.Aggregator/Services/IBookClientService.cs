using Book.Contracts;
namespace Library.Aggregator.Services;

public interface IBookClientService
{
    Task<List<GetBooksByIdsResponse>> GetBooksByIdsAsync(string ids);
    Task<GetBookAvailableBorrowResponse?> GetBookAvailableBorrowAsync(int bookId);
}