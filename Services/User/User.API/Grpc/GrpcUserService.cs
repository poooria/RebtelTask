using Grpc.Core;
using ProtoBuf.Grpc;
using User.API.Model;
using User.Contracts;

namespace User.API.Grpc;

public class GrpcUserService : IGrpcUserService
{
    private IUserRepository _userRepository;

    public GrpcUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetUsersByIdsResponse>> GetUsersByIds(GetUsersByIdsRequest request, CallContext context = default)
    {
        if (string.IsNullOrEmpty(request.Ids))
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Ids is null"));
        }
        var numIds = request.Ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

        if (!numIds.All(nid => nid.Ok))
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Ids must be comma-separated list of numbers"));
        }
        var idsToSelect = numIds
            .Select(id => id.Value);
        var users = await _userRepository.GetUsersByIdsAsync(idsToSelect);
        var userItems = users.Select(x => new GetUsersByIdsResponse
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Address = x.Address.ToString(),
            UniqId = x.UniqId,
            PhoneNumber = x.PhoneNumber,
            MembershipStartDate = x.MembershipStartDate

        }).ToList();
        return userItems;
    }
}