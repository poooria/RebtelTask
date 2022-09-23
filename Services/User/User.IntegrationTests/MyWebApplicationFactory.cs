using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using User.API.Infrastructure;

namespace User.IntegrationTests;

public class MyApplicationFactory : WebApplicationFactory<Program>

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<UserContext>));

            services.Remove(descriptor);

            services.AddDbContext<UserContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryIntegration");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<UserContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<MyApplicationFactory>>();
                db.Database.EnsureCreated();

            }
        });
    }


}