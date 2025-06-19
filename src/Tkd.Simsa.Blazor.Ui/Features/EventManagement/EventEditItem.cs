namespace Tkd.Simsa.Blazor.Ui.Features.EventManagement;

using Tkd.Simsa.Blazor.Ui.Features.Common;
using Tkd.Simsa.Domain.EventManagement;

public class EventEditItem : IEditItem<Event, EventEditItem>
{
    public required string Description { get; set; }

    public required string Name { get; set; }

    public required Event Source { get; init; }

    public required DateTime? StartDate { get; set; }

    public static EventEditItem FromModel(Event source)
        => new ()
        {
            Description = source.Description,
            Name = source.Name,
            StartDate = source.StartDate.ToDateTime(TimeOnly.MinValue),
            Source = source
        };

    public static EventEditItem New()
        => new ()
        {
            Description = string.Empty,
            Name = string.Empty,
            Source = Event.Empty,
            StartDate = null
        };

    public Event ToModel()
        => this.Source with
        {
            Description = this.Description,
            Name = this.Name,
            StartDate = DateOnly.FromDateTime(this.StartDate ?? default)
        };
}