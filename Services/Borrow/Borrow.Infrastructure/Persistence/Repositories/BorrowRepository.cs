namespace Borrow.Infrastructure.Persistence.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly BorrowContext _context;
    public BorrowRepository(BorrowContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public IQueryable<Core.Entities.BorrowedBook> GetAllBorrowed()
    {
        return _context.BorrowedBooks;
    }
    public IQueryable<Core.Entities.BorrowedBook> GetAllBorrowed(int bookId)
    {
        return _context.BorrowedBooks.Where(x => x.BookId == bookId);
    }
    public async Task<List<Core.Entities.BorrowedBook>> GetAllBorrowedByBookIdAsync(int bookId)
    {
        return await _context.BorrowedBooks.Where(x => x.BookId == bookId).ToListAsync();
    }

    public async Task<List<Core.Entities.BorrowedBook>> GetAllReturnedBooksByBookIdAsync(int bookId)
    {
        return await _context.BorrowedBooks.Where(x => x.BookId == bookId && x.ReturnDate.HasValue).ToListAsync();
        
    }

    public async Task<List<Core.Entities.BorrowedBook>> GetBorrowedBooksByUserIdAsync(int userId)
    {
        return await _context.BorrowedBooks.Where(x => x.UserId == userId).ToListAsync();
    }
}