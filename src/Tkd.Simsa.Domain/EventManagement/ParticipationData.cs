namespace Tkd.Simsa.Domain.EventManagement;

public record ParticipationData(IReadOnlyList<Participant> Participants)
{
    public static readonly ParticipationData NoParticipationData = new ([]);
}