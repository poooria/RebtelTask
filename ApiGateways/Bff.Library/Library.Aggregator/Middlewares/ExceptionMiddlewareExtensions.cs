using System.Net;
using Library.Aggregator.DTO.Responses;
using Library.Aggregator.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Library.Aggregator.Middlewares;
public static class ExceptionMiddlewareExtensions
{
    public static void UseRebtelExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(err =>
        {
            err.Run(async ctx =>
            {
                var exception = ctx.Features.Get<IExceptionHandlerFeature>();
                ctx.Response.ContentType = "application/json";
                if (exception != null)
                {
                    if (exception.Error is ResponseException)
                    {
                        var exceptionError = (ResponseException)exception.Error;
                        ctx.Response.StatusCode = (int)exceptionError.Status;
                        await ctx.Response.WriteAsync(new ErrorDetailResponse
                        {
                            StatusCode = (int)exceptionError.Status,
                            Message = exceptionError.Message,
                        }.ToString());
                    }
                    else
                    {
                        ctx.Response.StatusCode = 400;
                        await ctx.Response.WriteAsync(new ErrorDetailResponse
                        {
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Message = exception.Error.Message,
                        }.ToString());
                    }
                }
            });
        });
    }
}