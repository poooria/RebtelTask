using Library.Aggregator.DTO.Responses;
using MediatR;

namespace Library.Aggregator.DTO.Requests;

public class UserBorrowedBooksRequest : IRequest<IList<UserBorrowedBooksResponse>>
{
    public int UserId { get; set; }
}