using System.Runtime.Serialization;

namespace Book.Contracts;
[DataContract]
public class GetBooksByIdsResponse
{
    [DataMember(Order = 1)]
    public int Id { get; set; }
    [DataMember(Order = 2)]
    public string Title { get; set; }
    [DataMember(Order = 3)]
    public string BookAuthor { get; set; }
    [DataMember(Order = 4)]
    public string BookPublisher { get; set; }
    [DataMember(Order = 5)]
    public string BookLanguage { get; set; }
    [DataMember(Order = 6)]
    public double Weight { get; set; }
    [DataMember(Order = 7)]
    public string ISBN { get; set; }
    [DataMember(Order = 8)]
    public int Pages { get; set; }
    [DataMember(Order = 9)]
    public int TotalCopies { get; set; }
    [DataMember(Order = 10)]
    public int BorrowedCopies { get; set; }
    [DataMember(Order = 11)]
    public string Description { get; set; }
}