namespace Tkd.Simsa.DataGenerator;

using Bogus;

using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;

internal class FakerCollection
{
    public readonly Faker<Domain.EventManagement.Event> EventFaker;

    public readonly Dictionary<IFakerTInternal, object> GeneratedItems = new ();

    public readonly Faker<Domain.EventManagement.Participant> ParticipantFaker;

    public readonly Faker<Domain.PersonManagement.Person> PersonFaker;

    public FakerCollection()
    {
        this.PersonFaker = this.DefinePersonFaker();
        this.ParticipantFaker = this.DefineParticipantFaker();
        this.EventFaker = this.DefineEventFaker();
    }

    public List<T> GetItems<T>(Faker<T> faker)
        where T : class
    {
        return (List<T>)this.GeneratedItems[faker];
    }

    private Faker<Event> DefineEventFaker()
    {
        var faker = new Faker<Domain.EventManagement.Event>()
            .StrictMode(true)
            .RuleFor(i => i.Id, Guid.NewGuid)
            .RuleFor(i => i.Description, f => f.Lorem.Text())
            .RuleFor(i => i.Name, f => $"Exam {f.Random.Number(100)}")
            .RuleFor(i => i.ParticipationData, () => new ParticipationData(this.ParticipantFaker.GenerateBetween(3, 8)))
            .RuleFor(
                i => i.StartDate,
                f => f.Date.BetweenDateOnly(
                    DateOnly.FromDateTime(DateTime.UtcNow - TimeSpan.FromDays(365)),
                    DateOnly.FromDateTime(DateTime.UtcNow + TimeSpan.FromDays(30))));

        var generatedItems = faker.Generate(100);
        this.GeneratedItems[faker] = generatedItems;
        return faker;
    }

    private Faker<Participant> DefineParticipantFaker()
    {
        var faker = new Faker<Domain.EventManagement.Participant>()
            .StrictMode(false)
            .RuleFor(i => i.Id, Guid.NewGuid)
            .RuleFor(i => i.PersonId, f => f.PickRandom(this.GetItems(this.PersonFaker)).Id)
            .RuleFor(i => i.PersonInfo, (_, u) => this.GetItems(this.PersonFaker).Single(p => p.Id == u.PersonId));

        var generatedItems = faker.Generate(100);
        this.GeneratedItems[faker] = generatedItems;
        return faker;
    }

    private Faker<Domain.PersonManagement.Person> DefinePersonFaker()
    {
        var faker = new Faker<Domain.PersonManagement.Person>()
            .StrictMode(true)
            .RuleFor(i => i.Id, Guid.NewGuid)
            .RuleFor(i => i.Name, f => new PersonName(f.Person.FirstName, f.Person.LastName))
            .RuleFor(i => i.DateOfBirth, f => BirthDate.FromDateTime(f.Person.DateOfBirth))
            .RuleFor(i => i.Gender, f => f.PickRandom<Gender>());

        var generatedItems = faker.Generate(100);
        this.GeneratedItems[faker] = generatedItems;
        return faker;
    }
}