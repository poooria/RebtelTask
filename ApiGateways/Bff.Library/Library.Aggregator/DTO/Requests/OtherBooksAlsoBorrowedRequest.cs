using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class OtherBooksAlsoBorrowedRequest : IRequest<IList<OtherBooksAlsoBorrowedResponse>>
{
    public int BookId { get; set; }   
}