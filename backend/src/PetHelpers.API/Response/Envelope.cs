namespace PetHelpers.API.Response;

public record ResponceError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    private Envelope(object? result, IEnumerable<ResponceError> errors)
    {
        Result = result;
        Errors = errors.ToList();
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Success(object? result = null) => new(result, []);

    public static Envelope Error(IEnumerable<ResponceError> errors) => new(null, errors);

    public object? Result { get; }

    public List<ResponceError> Errors { get; }

    public DateTime TimeGenerated { get; }
}