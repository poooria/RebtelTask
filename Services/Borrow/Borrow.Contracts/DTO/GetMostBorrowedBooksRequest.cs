using System.Runtime.Serialization;
using MediatR;

namespace Borrow.Contracts.DTO;
[DataContract]
public class GetMostBorrowedBooksRequest : IRequest<IList<GetMostBorrowedBooksResponse>>
{

}