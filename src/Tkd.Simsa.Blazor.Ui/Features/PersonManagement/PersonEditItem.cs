namespace Tkd.Simsa.Blazor.Ui.Features.PersonManagement;

using Tkd.Simsa.Blazor.Ui.Features.Common;
using Tkd.Simsa.Domain.PersonManagement;

public class PersonEditItem : IEditItem<Person, PersonEditItem>
{
    public required DateTime? DateOfBirth { get; set; }

    public required string FirstName { get; set; }

    public required Gender? Gender { get; set; }

    public required string LastName { get; set; }

    public required Person Source { get; init; }

    public static PersonEditItem FromModel(Person source)
        => new ()
        {
            DateOfBirth = source.DateOfBirth.ToDateTime(),
            FirstName = source.Name.FirstName,
            Gender = source.Gender,
            LastName = source.Name.LastName,
            Source = source
        };

    public static PersonEditItem New()
        => new ()
        {
            DateOfBirth = null,
            FirstName = string.Empty,
            Gender = null,
            LastName = string.Empty,
            Source = Person.Empty
        };

    public Person ToModel()
        => this.Source with
        {
            Name = new PersonName(this.FirstName, this.LastName),
            DateOfBirth = BirthDate.FromDateTime(this.DateOfBirth ?? default),
            Gender = this.Gender ?? Domain.PersonManagement.Gender.Unknown
        };
}