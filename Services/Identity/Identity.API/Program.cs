using System.IO.Compression;
using Identity.API.Grpc;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
    builder.Services.AddEndpointsApiExplorer();
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
    endpoints.MapGrpcService<GrpcIdentityService>();
});
app.MapControllers();
app.Run();
public partial class Program { }