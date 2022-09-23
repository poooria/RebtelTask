namespace Borrow.Application.Persistence;

public interface IBorrowRepository
{
    IQueryable<Core.Entities.BorrowedBook> GetAllBorrowed();
    IQueryable<Core.Entities.BorrowedBook> GetAllBorrowed(int bookId);
    Task<List<Core.Entities.BorrowedBook>> GetAllBorrowedByBookIdAsync(int bookId);
    Task<List<Core.Entities.BorrowedBook>> GetAllReturnedBooksByBookIdAsync(int bookId);
    Task<List<Core.Entities.BorrowedBook>> GetBorrowedBooksByUserIdAsync(int userId);
}