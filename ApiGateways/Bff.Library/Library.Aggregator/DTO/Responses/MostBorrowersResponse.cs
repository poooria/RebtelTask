namespace Library.Aggregator.DTO.Responses;

public class MostBorrowersResponse
{
    public string UniqId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int BorrowedCount { get; set; }
}