using PetHelpers.Domain.Shared;

namespace PetHelpers.API.Response;

public record Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorMessage = error?.Message;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Success(object? result = null) => new(result, null);

    public static Envelope Error(Error error) => new(null, error);

    public object? Result { get; }

    public string? ErrorCode { get; }

    public string? ErrorMessage { get; }

    public DateTime TimeGenerated { get; }
}