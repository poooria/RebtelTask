namespace User.API.Model;

public class User
{
    public int Id { get; set; }
    public string UniqId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime MembershipStartDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Address Address { get; private set; }
    protected User()
    {

    }
    public User(string firstName, string lastName, string email, string phoneNumber, Address address)
    {
        UniqId = GenerateUniqId();
        FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
        LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
        Email = email;
        PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ? phoneNumber : throw new ArgumentNullException(nameof(phoneNumber)); ;
        Address = address;
        MembershipStartDate = DateTime.Now;
    }
    private string GenerateUniqId()
    {
        var now = DateTime.Now;
        var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
        int uniqueId = (int)(zeroDate.Ticks / 10000);
        return uniqueId.ToString();
    }
}