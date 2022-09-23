using System.Runtime.Serialization;

namespace Book.Contracts;
[DataContract]
public class GetBookAvailableBorrowRequest
{
    [DataMember(Order = 1)]
    public int BookId { get; set; }
}