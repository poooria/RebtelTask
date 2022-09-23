using Borrow.Contracts.DTO;

namespace Borrow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddMediatR(typeof(GetMostBorrowedBooksResponse).Assembly);
    }
}