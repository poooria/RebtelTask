using Borrow.Contracts.DTO;

namespace Borrow.Application.Queries;

public interface IGetBorrowedBooksByUserHandler : IRequestHandler<GetBorrowedBooksByUserRequest, IList<GetBorrowedBooksByUserResponse>>
{
    
}