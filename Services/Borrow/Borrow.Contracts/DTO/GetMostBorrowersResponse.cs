using System.Runtime.Serialization;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetMostBorrowersResponse
{
    [DataMember(Order = 1)]
    public int UserId { get; set; }
    [DataMember(Order = 2)]
    public int BorrowedCount { get; set; }
}