namespace Tkd.Simsa.Domain.EventManagement;

using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Domain.PersonManagement;

public record Participant : IModelWithId<Guid>
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public required Guid PersonId { get; init; }

    public required PersonInfo PersonInfo { get; init; }
}