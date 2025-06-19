namespace Tkd.Simsa.Persistence.Repositories;

using Tkd.Simsa.Application.PersonManagement;
using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence.Entities;
using Tkd.Simsa.Persistence.Mapper;

internal class PersonRepository : GenericRepository<PersonEntity, Person>, IPersonRepository
{
    public PersonRepository(SimsaDbContext dbContext, IMapper<PersonEntity, Person> mapper)
        : base(dbContext, mapper)
    {
    }
}