using System.Net;

namespace Library.Aggregator.Exceptions;

public class ResponseException : Exception
{
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; }
    public ResponseException(HttpStatusCode status, string message)
    {
        Status = status;
        Message = message;
    }
}