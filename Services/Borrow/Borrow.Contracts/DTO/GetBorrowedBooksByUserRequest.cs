using System.Runtime.Serialization;
using MediatR;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetBorrowedBooksByUserRequest : IRequest<IList<GetBorrowedBooksByUserResponse>>
{
    [DataMember(Order = 1)]
    public int UserId { get; set; }
}