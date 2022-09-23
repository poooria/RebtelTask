using System.Runtime.Serialization;

namespace User.Contracts;

[DataContract]
public class GetUsersByIdsRequest
{
    [DataMember(Order = 1)]
    public string Ids { get; set; }
}