using System.Runtime.Serialization;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetBorrowedBooksByUserResponse
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
    [DataMember(Order = 2)]
    public int UserId { get; set; }
    [DataMember(Order = 3)]
    public DateTime BorrowedDate { get; set; }
    [DataMember(Order = 4)]
    public DateTime? ReturnDate { get; set; }
}