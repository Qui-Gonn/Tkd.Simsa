namespace Tkd.Simsa.Domain.PersonManagement;

using Tkd.Simsa.Domain.Common;

public record Person : IModelWithId<Guid>
{
    public static readonly Person Empty = new ()
    {
        DateOfBirth = BirthDate.Empty,
        Gender = Gender.Unknown,
        Name = PersonName.Empty
    };

    public required BirthDate DateOfBirth { get; init; }

    public required Gender Gender { get; init; }

    public Guid Id { get; init; } = Guid.NewGuid();

    public required PersonName Name { get; init; }
}