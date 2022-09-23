using User.API.Model.Common;

namespace User.API.Model;

public class Address : ValueObject
{
    public String Country { get; private set; }
    public String Province { get; private set; }
    public String City { get; private set; }
    public String Street { get; private set; }
    public String ZipCode { get; private set; }

    public Address() { }
    public override string ToString()
    {
        return $"{Country}, {Province}, {City} {Street},{ZipCode}";
    }

    public Address(string country, string province, string city, string street, string zipcode)
    {
        Country = country;
        Province = province;
        City = !string.IsNullOrWhiteSpace(city) ? city : throw new ArgumentNullException(nameof(city)); ; ;
        Street = !string.IsNullOrWhiteSpace(street) ? street : throw new ArgumentNullException(nameof(street)); ; ;
        ZipCode = !string.IsNullOrWhiteSpace(zipcode) ? zipcode : throw new ArgumentNullException(nameof(zipcode)); ; ;
    }
}