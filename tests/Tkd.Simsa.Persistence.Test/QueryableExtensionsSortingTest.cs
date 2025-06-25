namespace Tkd.Simsa.Persistence.Test;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Extensions;
using Tkd.Simsa.Persistence.Test.Helper;

public class QueryableExtensionsSortingTest
{
    [Fact]
    public void ApplySorting_SortsAscendingAndDescending()
    {
        var data = new List<TestEntity>
        {
            new (Guid.NewGuid(), "B"),
            new (Guid.NewGuid(), "A"),
            new (Guid.NewGuid(), "C")
        };
        var queryable = data.AsQueryable();

        // Sort ascending by Value
        var sortAsc = new SortDescriptor<TestModel>(x => x.Value);
        var sortDesc = new SortDescriptor<TestModel>(x => x.Value, SortDirection.Descending);
        var sorters = new SortDescriptors<TestModel> { sortAsc };
        var mapper = MapperHelper.TestPropertyMapper;

        var sorted = queryable.ApplySorting(sorters, mapper).ToList();
        var expectedAsc = data.OrderBy(x => x.Value).Select(x => x.Value).ToList();
        Assert.Equal(expectedAsc, sorted.Select(x => x.Value).ToList());

        // Now test descending by Value
        sorters = new SortDescriptors<TestModel> { sortDesc };
        sorted = queryable.ApplySorting(sorters, mapper).ToList();
        var expectedDesc = data.OrderByDescending(x => x.Value).Select(x => x.Value).ToList();
        Assert.Equal(expectedDesc, sorted.Select(x => x.Value).ToList());
    }
}