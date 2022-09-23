using Book.Contracts;

namespace Library.Aggregator.Services;

public class BookClientService : IBookClientService
{
    private readonly IGrpcBookService _bookClient;

    public BookClientService(IGrpcBookService bookClient)
    {
        _bookClient = bookClient;
    }

    public async Task<List<GetBooksByIdsResponse>> GetBooksByIdsAsync(string ids)
    {
        return await _bookClient.GetBooksByIdsAsync(new GetBooksByIdsRequest { Ids = ids });
    }

    public async Task<GetBookAvailableBorrowResponse> GetBookAvailableBorrowAsync(int bookId)
    {

        return await _bookClient.GetBookAvailableBorrowAsync(new GetBookAvailableBorrowRequest { BookId = bookId });
    }
}