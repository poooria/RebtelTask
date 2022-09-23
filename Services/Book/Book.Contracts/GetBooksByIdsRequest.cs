using System.Runtime.Serialization;

namespace Book.Contracts;
[DataContract]
public class GetBooksByIdsRequest
{
    [DataMember(Order = 1)]
    public string Ids { get; set; }
}