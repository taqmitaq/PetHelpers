using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetHelpers.Infrastructure.Extensions;

public static class EFCorePropertyExtensions
{
    public static PropertyBuilder<DateTime> SetDefaultDateTimeKind(
        this PropertyBuilder<DateTime> builder, DateTimeKind kind)
    {
        return builder.HasConversion(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, kind));
    }
}