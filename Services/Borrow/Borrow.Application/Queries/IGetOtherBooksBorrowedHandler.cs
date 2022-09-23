using Borrow.Contracts.DTO;

namespace Borrow.Application.Queries;

public interface IGetOtherBooksBorrowedHandler : IRequestHandler<GetOtherBooksBorrowedRequest, IList<GetOtherBooksBorrowedResponse>>
{
    
}