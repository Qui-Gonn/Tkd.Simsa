namespace Tkd.Simsa.Persistence.Entities;

using Tkd.Simsa.Domain.Common;

internal class EventEntity : IHasId<Guid>
{
    public string Description { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ParticipationData { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }
}