namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;
using System.Text.Json;

using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Persistence.Entities;

internal class EventMapper : IMapper<EventEntity, Event>
{
    public EventEntity ToEntity(Event model)
        => this.UpdateEntity(new EventEntity { Id = model.Id }, model);

    public Expression<Func<EventEntity, object>> ToEntityPropertyExpression(string propertyName)
        => propertyName switch
        {
            nameof(Event.Name) => i => i.Name,
            nameof(Event.StartDate) => i => i.StartDate,
            _ => throw new NotSupportedException(propertyName)
        };

    public Event ToModel(EventEntity entity)
        => new ()
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Name,
            ParticipationData = JsonSerializer.Deserialize<ParticipationData>(entity.ParticipationData) ?? ParticipationData.NoParticipationData,
            StartDate = entity.StartDate
        };

    public EventEntity UpdateEntity(EventEntity entity, Event model)
    {
        entity.Description = model.Description;
        entity.Name = model.Name;
        entity.ParticipationData = JsonSerializer.Serialize(model.ParticipationData);
        entity.StartDate = model.StartDate;
        return entity;
    }
}