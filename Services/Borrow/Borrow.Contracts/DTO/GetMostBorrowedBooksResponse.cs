using System.Runtime.Serialization;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetMostBorrowedBooksResponse
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
    [DataMember(Order = 2)]
    public int BorrowedCount { get; set; }
}