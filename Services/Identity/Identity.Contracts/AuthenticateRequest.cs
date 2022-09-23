using System.Runtime.Serialization;

namespace Identity.Contracts;
[DataContract]
public class AuthenticateRequest
{
    [DataMember(Order = 1)]
    public string UserName { get; set; }
    [DataMember(Order = 2)]
    public string Password { get; set; }
}