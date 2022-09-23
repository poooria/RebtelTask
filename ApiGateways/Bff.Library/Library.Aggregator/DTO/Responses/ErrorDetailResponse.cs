using System.Text.Json;

namespace Library.Aggregator.DTO.Responses;

public class ErrorDetailResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}