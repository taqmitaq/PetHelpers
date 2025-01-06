namespace PetHelpers.API.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    private Envelope(object? result, IEnumerable<ResponseError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Success(object? result = null) => new(result, []);

    public static Envelope Error(IEnumerable<ResponseError> errors) => new(null, errors);

    public object? Result { get; }

    public List<ResponseError> Errors { get; }

    public DateTime TimeGenerated { get; }
}