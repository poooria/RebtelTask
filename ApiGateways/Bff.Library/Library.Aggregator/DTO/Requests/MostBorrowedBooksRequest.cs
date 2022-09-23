using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class MostBorrowedBooksRequest : IRequest<IList<MostBorrowedBooksResponse>>
{
    
}