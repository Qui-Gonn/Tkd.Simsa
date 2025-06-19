namespace Tkd.Simsa.Persistence.Entities;

using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Domain.PersonManagement;

internal class PersonEntity : IHasId<Guid>
{
    public DateOnly DateOfBirth { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public Guid Id { get; set; }

    public string LastName { get; set; } = string.Empty;
}