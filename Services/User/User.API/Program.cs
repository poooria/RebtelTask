using System.IO.Compression;
using ProtoBuf.Grpc.Server;
using User.API.Grpc;
using User.API.Infrastructure;
using User.API.Infrastructure.Repositories;
using User.API.Model;

var builder = WebApplication.CreateBuilder(args);
{
    
    builder.Services.AddControllers();
    builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddDbContext<UserContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddGrpc(options =>
    {
        options.EnableDetailedErrors = true;
    });
}
var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GrpcUserService>();
});
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<UserContext>();
        await context.SeedAsync();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}
app.MapControllers();
app.Run();
public partial class Program { }