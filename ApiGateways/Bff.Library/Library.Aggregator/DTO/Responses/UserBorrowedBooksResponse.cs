namespace Library.Aggregator.DTO.Responses;

public class UserBorrowedBooksResponse
{
    public string Title { get; set; }
    public DateTime BorrowedDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}