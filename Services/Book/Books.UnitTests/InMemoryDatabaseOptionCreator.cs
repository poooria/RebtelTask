using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book.UnitTests;

public  static  class InMemoryDatabaseOptionCreator
{
    public static DbContextOptions<BookContext> CreateNewContextOptions()
    {

        var service = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .AddEntityFrameworkProxies()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<BookContext>();
        builder.UseInMemoryDatabase("TestDB")
            .UseInternalServiceProvider(service);
        return builder.Options;
    }
}