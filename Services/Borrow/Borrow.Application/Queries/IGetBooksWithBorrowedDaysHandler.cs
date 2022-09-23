using Borrow.Contracts.DTO;

namespace Borrow.Application.Queries;

public interface IGetBooksWithBorrowedDaysHandler : IRequestHandler<GetBooksWithBorrowedDaysRequest, IList<GetBooksWithBorrowedDaysResponse>>
{

}