namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;

using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence.Entities;

internal class PersonMapper : IMapper<PersonEntity, Person>
{
    public PersonEntity ToEntity(Person model)
        => this.UpdateEntity(new PersonEntity { Id = model.Id }, model);

    public Expression<Func<PersonEntity, object>> ToEntityPropertyExpression(string propertyName)
        => propertyName switch
        {
            nameof(Person.Name.FirstName) => i => i.FirstName,
            nameof(Person.Name.LastName) => i => i.LastName,
            _ => throw new NotSupportedException(propertyName)
        };

    public Person ToModel(PersonEntity entity)
        => new ()
        {
            Id = entity.Id,
            DateOfBirth = BirthDate.FromDateOnly(entity.DateOfBirth),
            Gender = entity.Gender,
            Name = new PersonName(entity.FirstName, entity.LastName)
        };

    public PersonEntity UpdateEntity(PersonEntity entity, Person model)
    {
        entity.DateOfBirth = model.DateOfBirth.Date;
        entity.FirstName = model.Name.FirstName;
        entity.LastName = model.Name.LastName;
        entity.Gender = model.Gender;
        return entity;
    }
}