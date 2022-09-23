using System;
using System.Linq;
using Borrow.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Borrow.IntegrationTests;

public class MyApplicationFactory : WebApplicationFactory<Program>

{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<BorrowContext>));

            services.Remove(descriptor);

            services.AddDbContext<BorrowContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryIntegration");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BorrowContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<MyApplicationFactory>>();
                db.Database.EnsureCreated();

            }
        });
    }


}