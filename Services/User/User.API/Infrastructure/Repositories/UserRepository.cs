using User.API.Model;

namespace User.API.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;
    public UserRepository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<Model.User>> GetUsersByIdsAsync(IEnumerable<int> usersIds)
    {
        return await _context.Users.Where(x => usersIds.Contains(x.Id)).ToListAsync();
    }
}