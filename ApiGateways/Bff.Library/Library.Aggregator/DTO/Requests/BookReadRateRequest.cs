using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class BookReadRateRequest : IRequest<BookReadRateResponse>
{
    public int BookId { get; set; }   
}