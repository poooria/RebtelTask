using System.ServiceModel;
using ProtoBuf.Grpc;

namespace User.Contracts;
[ServiceContract(Name = "User.GrpcUserServiceBase")]
public interface IGrpcUserService
{
    [OperationContract]
    Task<List<GetUsersByIdsResponse>> GetUsersByIds(GetUsersByIdsRequest request, CallContext context = default);
}