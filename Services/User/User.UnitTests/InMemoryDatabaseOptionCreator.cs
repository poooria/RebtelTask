using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.UnitTests;

public static class InMemoryDatabaseOptionCreator
{
    public static DbContextOptions<UserContext> CreateNewContextOptions()
    {

        var service = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .AddEntityFrameworkProxies()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<UserContext>();
        builder.UseInMemoryDatabase("TestDB")
            .UseInternalServiceProvider(service);
        return builder.Options;
    }
}