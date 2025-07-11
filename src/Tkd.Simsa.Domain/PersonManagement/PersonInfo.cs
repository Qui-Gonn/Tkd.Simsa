namespace Tkd.Simsa.Domain.PersonManagement;

public record PersonInfo
{
    public BirthDate DateOfBirth { get; init; } = BirthDate.Empty;

    public Gender Gender { get; init; } = Gender.Unknown;

    public PersonName Name { get; init; } = PersonName.Empty;
}