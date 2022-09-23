using System.Runtime.Serialization;

namespace User.Contracts;
[DataContract]
public class GetUsersByIdsResponse
{
    [DataMember(Order = 1)]
    public int Id { get; set; }
    [DataMember(Order = 2)]
    public string UniqId { get; set; }
    [DataMember(Order = 3)]
    public string FirstName { get; set; }
    [DataMember(Order = 4)]
    public string LastName { get; set; }
    [DataMember(Order = 5)]
    public DateTime MembershipStartDate { get; set; }
    [DataMember(Order = 6)]
    public string Email { get; set; }
    [DataMember(Order = 7)]
    public string PhoneNumber { get; set; }
    [DataMember(Order = 8)]
    public string Address { get; set; }
}