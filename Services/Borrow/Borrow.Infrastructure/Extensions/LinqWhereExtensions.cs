namespace Borrow.Infrastructure.Extensions;
public static class LinqWhereExpressions
{
    public static IQueryable<Core.Entities.BorrowedBook> WhereInTimeFrame(this IQueryable<Core.Entities.BorrowedBook> enumerable, DateTime startDate,DateTime endDate)
    {
        return enumerable.Where(x => (x.BorrowedDate >= startDate && x.BorrowedDate <= endDate ||
                                      x.ReturnDate > startDate && x.ReturnDate <= endDate ||
                                      x.BorrowedDate <= startDate && x.ReturnDate >= endDate));
    }
}