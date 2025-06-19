namespace Tkd.Simsa.Domain.EventManagement;

public record ParticipationData(List<Participant> Participants)
{
    public static readonly ParticipationData NoParticipationData = new ([]);
}