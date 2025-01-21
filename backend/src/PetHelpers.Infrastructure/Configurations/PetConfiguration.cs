using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelpers.Application.Dtos;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;
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


        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.Property(p => p.IsCastrated)
            .IsRequired();

        builder.Property(p => p.IsVaccinated)
            .IsRequired();

        builder.Property(p => p.BirthDate)
            .IsRequired();

        builder.Property(p => p.CreationDate)
            .SetDefaultDateTimeKind(DateTimeKind.Utc)
            .IsRequired();

        builder.Property(p => p.Requisites)
            .HasConversion(
                requisites => JsonSerializer.Serialize(
                    requisites.Select(
                        r => new RequisiteDto(r.Title.Text, r.Description.Text)),
                    JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<RequisiteDto>>(json, JsonSerializerOptions.Default)!
                    .Select(dto => new Requisite(Title.Create(dto.Title).Value, Description.Create(dto.Description).Value))
                    .ToList(),
                new ValueComparer<IReadOnlyList<Requisite>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb")
            .HasColumnName("requisites");

        builder.ComplexProperty(p => p.PetName, b =>
        {
            b.IsRequired();
            b.Property(pn => pn.Value)
                .HasColumnName("name")
                .HasMaxLength(Constants.MAX_SHORT_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Description, b =>
        {
            b.IsRequired();
            b.Property(p => p.Text)
                .HasColumnName("description")
                .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.Color, b =>
        {
            b.IsRequired();
            b.Property(c => c.Value)
                .HasColumnName("color");
        });

        builder.ComplexProperty(p => p.HealthInfo, b =>
        {
            b.IsRequired();
            b.Property(c => c.Info)
                .HasColumnName("health_info")
                .HasMaxLength(Constants.MAX_LONG_TEXT_LENGTH);
        });

        builder.ComplexProperty(p => p.SpeciesAndBreed, b =>
        {
            b.IsRequired();
            b.Property(sb => sb.SpeciesId)
                .HasColumnName("species_id");
            b.Property(sb => sb.BreedId)
                .HasColumnName("breed_id");
        });

        builder.ComplexProperty(p => p.OwnersPhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number)
                .HasColumnName("owners_phone_number");
        });

        builder.ComplexProperty(p => p.HelpStatus, b =>
        {
            b.IsRequired();
            b.Property(s => s.Value)
                .HasColumnName("help_status");
        });

        builder.ComplexProperty(p => p.Location, b =>
        {
            b.IsRequired();
            b.Property(l => l.City)
                .HasColumnName("city")
                .HasMaxLength(Constants.MAX_SHORT_TEXT_LENGTH);
            b.Property(l => l.Region)
                .HasColumnName("region")
                .HasMaxLength(Constants.MAX_SHORT_TEXT_LENGTH);
            b.Property(l => l.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(Constants.MAX_SHORT_TEXT_LENGTH);
        });
    }
}