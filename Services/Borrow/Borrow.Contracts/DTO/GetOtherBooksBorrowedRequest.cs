using System.Runtime.Serialization;
using MediatR;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetOtherBooksBorrowedRequest : IRequest<IList<GetOtherBooksBorrowedResponse>>
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
}