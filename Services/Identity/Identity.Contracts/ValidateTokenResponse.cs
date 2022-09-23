using System.Runtime.Serialization;

namespace Identity.Contracts;
[DataContract]
public class ValidateTokenResponse
{
    [DataMember(Order = 1)]
    public bool Result { get; set; }
}