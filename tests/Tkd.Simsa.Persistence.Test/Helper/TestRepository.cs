namespace Tkd.Simsa.Persistence.Test.Helper;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Persistence.Mapper;
using Tkd.Simsa.Persistence.Repositories;

internal class TestRepository(DbContext dbContext, IMapper<TestEntity, TestModel> mapper)
    : GenericRepository<TestEntity, TestModel>(dbContext, mapper);