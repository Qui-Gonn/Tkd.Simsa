namespace Tkd.Simsa.Domain.PersonManagement;

using Tkd.Simsa.Domain.Common;

public record Person : PersonInfo, IModelWithId<Guid>
{
    public static readonly Person Empty = new ()
    {
        DateOfBirth = BirthDate.Empty,
        Gender = Gender.Unknown,
        Name = PersonName.Empty
    };

    public Guid Id { get; init; } = Guid.NewGuid();
}