using System.ServiceModel;
using ProtoBuf.Grpc;

namespace Book.Contracts;
[ServiceContract(Name = "Book.GrpcBookServiceBase")]
public interface IGrpcBookService
{
    [OperationContract]
    Task<List<GetBooksByIdsResponse>> GetBooksByIdsAsync(GetBooksByIdsRequest request, CallContext context = default);
    [OperationContract]
    Task<GetBookAvailableBorrowResponse> GetBookAvailableBorrowAsync(GetBookAvailableBorrowRequest request, CallContext context = default);
}