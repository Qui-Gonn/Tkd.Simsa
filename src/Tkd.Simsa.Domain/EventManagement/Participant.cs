namespace Tkd.Simsa.Domain.EventManagement;

using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Domain.PersonManagement;

public record Participant : IModelWithId<Guid>
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required Person PersonSnapshot { get; init; }
}