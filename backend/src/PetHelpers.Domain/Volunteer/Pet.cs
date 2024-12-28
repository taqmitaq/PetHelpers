using CSharpFunctionalExtensions;
using PetHelpers.Domain.Shared.Ids;

namespace PetHelpers.Domain.Volunteer;

public sealed class Pet : Entity<PetId>
{
    private readonly List<Requisite> _requisites = [];
    
    public Volunteer Volunteer { get; private set; }
    
    private Pet() { }
    
    public SpeciesAndBreed SpeciesAndBreed { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string HealthInfo { get; private set; }
    public string Location { get; private set; }
    public PhoneNumber OwnersPhoneNumber { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    public DateOnly Birthdate { get; private set; }
    public DateTime CreationDate { get; private set; }
    public Status HelpStatus { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;
}