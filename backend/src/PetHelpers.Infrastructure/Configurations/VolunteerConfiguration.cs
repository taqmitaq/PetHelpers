using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHelpers.Domain.Shared;
using PetHelpers.Domain.Shared.Ids;
using PetHelpers.Domain.Volunteer;
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

        builder.Property(v => v.Email)
                    .IsRequired();

        builder.Property(v => v.Description)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();

        builder.Property(v => v.Requisites)
            .JsonValueObjectCollectionConversion();

        builder.Property(v => v.SocialMedias)
            .JsonValueObjectCollectionConversion();

        builder.ComplexProperty(v => v.FullName, b =>
        {
            b.IsRequired();
            b.Property(v => v.FirstName).HasColumnName("first_name");
            b.Property(v => v.LastName).HasColumnName("last_name");
        });

        builder.ComplexProperty(v => v.PhoneNumber, b =>
        {
            b.IsRequired();
            b.Property(v => v.Number).HasColumnName("phone_number");
        });
    }
}