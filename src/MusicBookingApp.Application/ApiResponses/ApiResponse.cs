using System.Text.Json;

namespace MusicBookingApp.Application.ApiResponses;

public class ApiResponse<T>
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    public bool Success { get; init; }

    public string Message { get; init; }

    public string Note { get; init; }

    public T? Data { get; init; }

    public ApiResponse(T? data, string message, bool success)
    {
        Success = success;
        Message = message;
        Note = "N/A";
        Data = data;
    }
}