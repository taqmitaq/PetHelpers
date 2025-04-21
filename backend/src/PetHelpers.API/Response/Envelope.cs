using PetHelpers.Domain.Shared;

namespace PetHelpers.API.Response;

public record Envelope
{
    private Envelope(object? result, ErrorList errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Success(object? result = null) => new(result, null);

    public static Envelope Error(ErrorList errors) => new(null, errors);

    public object? Result { get; }

    public ErrorList? Errors { get; }

    public DateTime TimeGenerated { get; }
}