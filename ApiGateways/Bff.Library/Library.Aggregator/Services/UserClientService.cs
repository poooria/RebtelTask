using User.Contracts;

namespace Library.Aggregator.Services;

public class UserClientService : IUserClientService
{
    private readonly IGrpcUserService _userClient;

    public UserClientService(IGrpcUserService userClient)
    {
        _userClient = userClient;
    }
    public async Task<List<GetUsersByIdsResponse>> GetUsersByIdsAsync(string ids)
    {
        return await _userClient.GetUsersByIds(new GetUsersByIdsRequest() { Ids = ids });
    }
}