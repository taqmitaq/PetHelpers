namespace PetHelpers.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";

            return Error.Validation("value.is.invalid", $"{label} cannot be empty");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id is null ? string.Empty : $" for id {id}";

            return Error.Validation("record.not.found", $"record not found{forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name is null ? string.Empty : $" {name}";

            return Error.Validation("length.is.invalid", $"invalid{label} length");
        }
    }

    public static class Species
    {
        public static Error AlreadyExists()
        {
            return Error.Validation("record.already.exists", $"Species already exists");
        }
    }
}