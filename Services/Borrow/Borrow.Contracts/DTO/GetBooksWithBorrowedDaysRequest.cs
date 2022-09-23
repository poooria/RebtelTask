using System.Runtime.Serialization;
using MediatR;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetBooksWithBorrowedDaysRequest : IRequest<IList<GetBooksWithBorrowedDaysResponse>>
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
}