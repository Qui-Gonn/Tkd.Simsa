namespace Tkd.Simsa.Domain.EventManagement;

using Tkd.Simsa.Domain.Common;

public record Event : IModelWithId<Guid>
{
    public static readonly Event Empty = new ()
    {
        Description = string.Empty,
        Name = string.Empty,
        StartDate = DateOnly.MinValue
    };

    public string Description { get; init; } = string.Empty;

    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = string.Empty;

    public ParticipationData ParticipationData { get; init; } = ParticipationData.NoParticipationData;

    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);
}