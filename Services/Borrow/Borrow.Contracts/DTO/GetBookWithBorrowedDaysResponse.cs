using System.Runtime.Serialization;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetBooksWithBorrowedDaysResponse
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
    [DataMember(Order = 2)]
    public int UserId { get; set; }
    [DataMember(Order = 3)]
    public double BorrowedDaysCount { get; set; }
}