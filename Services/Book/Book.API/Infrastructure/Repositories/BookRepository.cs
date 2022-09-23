namespace Book.API.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    public BookRepository(BookContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Model.Book> GetBookAsync(int bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
    }

    public async Task<List<Model.Book>> GetBooksByIdsAsync(IEnumerable<int> booksIds)
    {
        return await _context.Books.Where(x => booksIds.Contains(x.Id)).ToListAsync();
    }
}