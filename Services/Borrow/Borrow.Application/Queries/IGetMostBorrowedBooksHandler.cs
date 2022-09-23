using Borrow.Contracts.DTO;

namespace Borrow.Application.Queries;

public interface IGetMostBorrowedBooksHandler : IRequestHandler<GetMostBorrowedBooksRequest, IList<GetMostBorrowedBooksResponse>>
{

}