using Borrow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Borrow.UnitTests;

public static class InMemoryDatabaseOptionCreator
{
    public static DbContextOptions<BorrowContext> CreateNewContextOptions()
    {

        var service = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .AddEntityFrameworkProxies()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<BorrowContext>();
        builder.UseInMemoryDatabase("TestDB")
            .UseInternalServiceProvider(service);
        return builder.Options;
    }
}