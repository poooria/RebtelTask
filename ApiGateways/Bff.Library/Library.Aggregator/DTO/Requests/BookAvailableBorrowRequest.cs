using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class BookAvailableBorrowRequest : IRequest<BookAvailableBorrowResponse>
{
    public int BookId { get; set; }
}