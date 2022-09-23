using System.ServiceModel;
using Borrow.Contracts.DTO;
using ProtoBuf.Grpc;

namespace Borrow.Contracts.Services;
[ServiceContract(Name = "Borrow.GrpcBorrowServiceBase")]
public interface IGrpcBorrowService
{
    [OperationContract]
    Task<List<GetMostBorrowedBooksResponse>> GetMostBorrowedBooksAsync(GetMostBorrowedBooksRequest request, CallContext context = default);
    [OperationContract]
    Task<List<GetMostBorrowersResponse>> GetMostBorrowersAsync(GetMostBorrowersRequest request, CallContext context = default);
    [OperationContract]
    Task<List<GetOtherBooksBorrowedResponse>> GetOtherBooksAlsoBorrowedAsync(GetOtherBooksBorrowedRequest request, CallContext context = default);
    [OperationContract]
    Task<List<GetBooksWithBorrowedDaysResponse>> GetBookWithBorrowedDaysAsync(GetBooksWithBorrowedDaysRequest request, CallContext context = default);
    [OperationContract]
    Task<List<GetBorrowedBooksByUserResponse>> GetBorrowedBooksByUserIdAsync(GetBorrowedBooksByUserRequest request, CallContext context = default);
}