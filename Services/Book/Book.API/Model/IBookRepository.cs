namespace Book.API.Model;

public interface IBookRepository
{
    Task<Book> GetBookAsync(int bookId);
    Task<List<Book>> GetBooksByIdsAsync(IEnumerable<int> booksIds);
}