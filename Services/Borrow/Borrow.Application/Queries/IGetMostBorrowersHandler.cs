using Borrow.Contracts.DTO;

namespace Borrow.Application.Queries;

public interface IGetMostBorrowersHandler : IRequestHandler<GetMostBorrowersRequest, IList<GetMostBorrowersResponse>>
{
    
}