using Library.Aggregator.DTO.Requests;
using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.Abstractions.Queries;

public interface IGetBookReadRateHandler : IRequestHandler<BookReadRateRequest, BookReadRateResponse>
{
    
}