namespace Library.Aggregator.DTO.Responses;

public class BookReadRateResponse
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public int BorrowedCount { get; set; }
    public int PagesPerDay { get; set; }
}