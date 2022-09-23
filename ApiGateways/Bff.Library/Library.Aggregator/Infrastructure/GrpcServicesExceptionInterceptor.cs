using System.Net;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Library.Aggregator.Exceptions;

namespace Library.Aggregator.Infrastructure;

public class GrpcServicesExceptionInterceptor : Interceptor
{
    private readonly ILogger<GrpcServicesExceptionInterceptor> _logger;

    public GrpcServicesExceptionInterceptor(ILogger<GrpcServicesExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var call = continuation(request, context);

        return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync,context), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
    }

    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> task, object clientInterceptorContext)
    {
        try
        {
            var response = await task;
            return response;
        }
        catch (RpcException e)
        {
            _logger.LogError("Error grpc calling: {Status} - {Message}", e.Status, e.Message);
            throw new ResponseException(HttpStatusCode.BadRequest, e.Status.Detail);
        }
    }
}
