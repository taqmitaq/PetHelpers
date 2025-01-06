using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelpers.Application.Dtos;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Domain.Volunteer.ValueObjects;

namespace PetHelpers.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.HasMany(v => v.OwnedPets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(v => v.YearsOfExperience)
            .IsRequired();

        builder.Property(v => v.PetsFoundHome)
            .IsRequired();

        builder.Property(v => v.PetsLookingForHome)
            .IsRequired();

        builder.Property(v => v.PetsInTreatment)
            .IsRequired();

        builder.Property(v => v.Requisites)
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

        builder.Property(v => v.SocialMedias)
            .HasConversion(
                socialMedias => JsonSerializer.Serialize(
                    socialMedias.Select(
                        r => new SocialMediaDto(r.Title.Text, r.Link.Text)),
                    JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<List<SocialMediaDto>>(json, JsonSerializerOptions.Default)!
                    .Select(dto => new SocialMedia(Title.Create(dto.Title).Value, Link.Create(dto.Link).Value))
                    .ToList(),
                new ValueComparer<IReadOnlyList<SocialMedia>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                    c => c.ToList()))
            .HasColumnType("jsonb")
            .HasColumnName("social_medias");

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.IsRequired();
            b.Property(fn => fn.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            b.Property(fn => fn.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        });

        builder.ComplexProperty(v => v.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(p => p.Number)
                .HasColumnName("phone_number");
        });

        builder.ComplexProperty(v => v.Email, b =>
        {
            b.IsRequired();
            b.Property(e => e.Address)
                .HasColumnName("email");
        });

        builder.ComplexProperty(v => v.Description, b =>
        {
            b.IsRequired();
            b.Property(d => d.Text)
                .HasColumnName("description")
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        });
    }
}