using System.Net;
using System.Reflection;
using Book.Contracts;
using Borrow.Contracts.Services;
using Identity.Contracts;
using Library.Aggregator.Authentication;
using Library.Aggregator.Infrastructure;
using Library.Aggregator.Middlewares;
using Library.Aggregator.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.ClientFactory;
using Swashbuckle.AspNetCore.Filters;
using User.Contracts;

namespace Library.Aggregator;

public class StartUp
{
    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer()
            .AddServices()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddGrpcServices(Configuration)
            .AddSwagger(Configuration)
            .AddCustomAuthentication(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRebtelExceptionHandler();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });

    }
}

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBorrowClientService, BorrowClientService>()
            .AddScoped<IBookClientService, BookClientService>()
            .AddScoped<IUserClientService, UserClientService>()
            .AddScoped<ITokenValidatorService, TokenValidatorService>();
        return services;
    }
    public static IServiceCollection AddGrpcServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<GrpcServicesExceptionInterceptor>();
        services.AddCodeFirstGrpcClient<IGrpcBorrowService>(options =>
        {
            options.Address = new Uri(configuration["Urls:Borrow"]);
        }).AddInterceptor<GrpcServicesExceptionInterceptor>();
        services.AddCodeFirstGrpcClient<IGrpcBookService>(options =>
        {
            options.Address = new Uri(configuration["Urls:Book"]);
        }).AddInterceptor<GrpcServicesExceptionInterceptor>();
        services.AddCodeFirstGrpcClient<IGrpcUserService>(options =>
        {
            options.Address = new Uri(configuration["Urls:User"]);
        }).AddInterceptor<GrpcServicesExceptionInterceptor>();
        services.AddCodeFirstGrpcClient<IGrpcIdentityService>(options =>
        {
            options.Address = new Uri(configuration["Urls:Identity"]);
        }).AddInterceptor<GrpcServicesExceptionInterceptor>();
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Rebtel API Aggregator", Version = "v1", Description = "First, call /api/authenticate to get authorization token and then set header authorization JWT token(Bearer)" });
            swagger.EnableAnnotations();
            swagger.ExampleFilters();
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "Library.Aggregator.xml");
            swagger.IncludeXmlComments(filePath);
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}

                }
            });
        });
        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(op =>
        {
            op.Events = new JwtBearerEvents()
            {
                OnMessageReceived = async context =>
                {
                    var endPoint = context.HttpContext.GetEndpoint();
                    if (endPoint != null)
                    {
                        var authorizeAttr = endPoint.Metadata.OfType<RebtelAuthorizeAttribute>();
                        if (authorizeAttr.Any())
                        {
                            var validator =
                                context.HttpContext.RequestServices.GetRequiredService<ITokenValidatorService>();
                            var authorizationHeaders = context.HttpContext.Request.Headers["Authorization"];
                            if (!authorizationHeaders.Any())
                            {
                                WriteUnauthorizeResponse(context);
                            }
                            var token = authorizationHeaders[0];
                            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            {
                                context.Token = token.Substring("Bearer ".Length).Trim();
                            }

                            if (string.IsNullOrEmpty(context.Token))
                            {
                                WriteUnauthorizeResponse(context);
                            }
                            var ok = await validator.ValidateTokenAsync(context.Token);
                            if (!ok.Result)
                            {
                                WriteUnauthorizeResponse(context);
                            }
                        }
                    }
                }
            };
        });
        return services;
    }

    private async static void WriteUnauthorizeResponse(MessageReceivedContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        await context.HttpContext.Response.WriteAsync(
            "Token Validation Has Failed. Request Access Denied");
    }
}