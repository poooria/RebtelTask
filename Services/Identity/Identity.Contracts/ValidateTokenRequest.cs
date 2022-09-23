using System.Runtime.Serialization;

namespace Identity.Contracts;
[DataContract]
public class ValidateTokenRequest
{
    [DataMember(Order = 1)]
    public string Token { get; set; }
}