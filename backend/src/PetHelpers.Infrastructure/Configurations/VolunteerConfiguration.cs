using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer.Entities;
using PetHelpers.Infrastructure.Extensions;

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
            .JsonValueObjectCollectionConversion();

        builder.Property(v => v.SocialMedias)
            .JsonValueObjectCollectionConversion();

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
                .HasColumnName("address");
        });

        builder.ComplexProperty(v => v.Description, b =>
        {
            b.IsRequired();
            b.Property(d => d.Text)
                .HasColumnName("description");
        });
    }
}