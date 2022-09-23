using System.Runtime.Serialization;
using MediatR;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetMostBorrowersRequest : IRequest<IList<GetMostBorrowersResponse>>
{
    [DataMember(Order = 1)]
    public DateTime StartDate { get; set; }
    [DataMember(Order = 2)]
    public DateTime EndDate { get; set; }
}