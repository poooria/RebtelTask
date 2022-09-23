using Borrow.Contracts.Services;
using Microsoft.Extensions.Configuration;

namespace Borrow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,ConfigurationManager configuration)
    {
        return services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddScoped<IBorrowRepository, BorrowRepository>()
            .AddDbContext<BorrowContext>(option => option.UseSqlServer(configuration.GetConnectionString("ConnectionString")));
    }
}