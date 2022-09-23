using System.ServiceModel;
using ProtoBuf.Grpc;

namespace Identity.Contracts;
[ServiceContract(Name = "Identity.GrpcIdentityServiceBase")]
public interface IGrpcIdentityService
{
    [OperationContract]
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CallContext context = default);
    Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, CallContext context = default);
}