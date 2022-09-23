namespace Library.Aggregator.DTO.Responses;

public class MostBorrowedBooksResponse
{
    public string Title { get; set; }
    public int TotalCopies { get; set; }
    public int BookId { get; set; }
    public int BorrowedCount { get; set; }
}