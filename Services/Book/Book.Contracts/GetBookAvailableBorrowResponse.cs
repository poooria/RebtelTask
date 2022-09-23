using System.Runtime.Serialization;

namespace Book.Contracts;
[DataContract]
public class GetBookAvailableBorrowResponse
{
    [DataMember(Order = 1)]
    public int Available { get; set; }
    [DataMember(Order = 2)]
    public int Borrowed { get; set; }
}