namespace Library.Aggregator.DTO.Responses;

public class OtherBooksAlsoBorrowedResponse
{
    public string Title { get; set; }
    public int BookId { get; set; }
    public int BorrowedCount { get; set; }
}