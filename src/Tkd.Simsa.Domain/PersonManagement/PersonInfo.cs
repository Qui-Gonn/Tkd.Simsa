namespace Tkd.Simsa.Domain.PersonManagement;

public record PersonInfo
{
    public required BirthDate DateOfBirth { get; init; }

    public required Gender Gender { get; init; }

    public required PersonName Name { get; init; }
}