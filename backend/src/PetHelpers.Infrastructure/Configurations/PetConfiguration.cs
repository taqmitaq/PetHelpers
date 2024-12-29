using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer;
using PetHelpers.Infrastructure.Extensions;

namespace PetHelpers.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.Property(p => p.Name)
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();

        builder.Property(p => p.Color)
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
            .IsRequired();

        builder.Property(p => p.HealthInfo)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();

        builder.Property(p => p.Location)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.Property(p => p.IsCastrated)
            .IsRequired();

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.Property(p => p.Birthdate)
            .IsRequired();

        builder.Property(p => p.CreationDate)
            .SetDefaultDateTimeKind(DateTimeKind.Utc)
            .IsRequired();

        builder.Property(p => p.Requisites)
                    .JsonValueObjectCollectionConversion();

        builder.ComplexProperty(p => p.SpeciesAndBreed, b =>
        {
            b.IsRequired();
            b.Property(p => p.SpeciesId)
                .HasColumnName("species_id");
            b.Property(p => p.BreedId)
                .HasColumnName("breed_id");
        });

        builder.ComplexProperty(p => p.OwnersPhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number)
                .HasColumnName("owners_phone_number")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.HelpStatus, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasColumnName("help_status");
        });
    }
}