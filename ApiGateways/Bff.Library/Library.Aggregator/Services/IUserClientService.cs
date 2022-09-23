using User.Contracts;

namespace Library.Aggregator.Services;

public interface IUserClientService
{
    Task<List<GetUsersByIdsResponse>> GetUsersByIdsAsync(string ids);
}