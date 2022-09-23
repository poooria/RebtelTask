using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class MostBorrowersRequest : IRequest<IList<MostBorrowersResponse>>
{
    /// <summary>
    /// Example : 2022-01-01
    /// </summary>
    public DateTime StartDate { get; set; }
    /// <summary>
    /// Example : 2022-12-01
    /// </summary>
    public DateTime EndDate { get; set; }
}