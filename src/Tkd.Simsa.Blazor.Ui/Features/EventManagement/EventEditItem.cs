namespace Tkd.Simsa.Blazor.Ui.Features.EventManagement;

using Tkd.Simsa.Blazor.Ui.Features.Common;
using Tkd.Simsa.Domain.EventManagement;

public class EventEditItem : IEditItem<Event, EventEditItem>
{
    public required string Description { get; set; }

    public required string Name { get; set; }

    public required List<Participant> Participants { get; set; } = [];

    public required Event Source { get; init; }

    public required DateTime? StartDate { get; set; }

    public static EventEditItem FromModel(Event source)
        => new ()
        {
            Description = source.Description,
            Name = source.Name,
            Participants = source.ParticipationData.Participants.ToList(),
            StartDate = source.StartDate.ToDateTime(TimeOnly.MinValue),
            Source = source
        };

    public static EventEditItem New()
        => FromModel(new Event());

    public Event ToModel()
        => this.Source with
        {
            Description = this.Description,
            Name = this.Name,
            ParticipationData = new ParticipationData(this.Participants),
            StartDate = DateOnly.FromDateTime(this.StartDate ?? default)
        };
}