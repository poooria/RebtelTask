using System.IO.Compression;
using Borrow.API.Grpc;
using Borrow.Application;
using Borrow.Contracts.Services;
using Borrow.Infrastructure;
using Borrow.Infrastructure.Persistence;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddGrpc(options =>
    {
        options.EnableDetailedErrors = true;
    });
}
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<BorrowContext>();
        await context.SeedAsync();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GrpcBorrowService>();
});
app.MapControllers();
app.Run();
public partial class Program { }