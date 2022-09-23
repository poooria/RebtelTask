namespace Book.API.Model;

public class BookAuthor : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
}