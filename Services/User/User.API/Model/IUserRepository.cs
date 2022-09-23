namespace User.API.Model;

public interface IUserRepository
{
    Task<List<User>> GetUsersByIdsAsync(IEnumerable<int> usersIds);
}