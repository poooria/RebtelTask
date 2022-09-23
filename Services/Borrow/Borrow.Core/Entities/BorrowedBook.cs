using Borrow.Core.Entities.Common;

namespace Borrow.Core.Entities;

public class BorrowedBook : BaseEntity
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}