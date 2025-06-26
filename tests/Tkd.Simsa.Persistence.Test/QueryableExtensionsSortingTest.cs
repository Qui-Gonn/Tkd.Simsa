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
        var sortAsc = Sort.By<TestModel>(x => x.Value);
        var sortDesc = Sort.By<TestModel>(x => x.Value, SortDirection.Descending);
        var mapper = MapperHelper.TestPropertyMapper;

        var sorted = queryable.ApplySorting(sortAsc.ToSortDescriptors(), mapper).ToList();
        var expectedAsc = data.OrderBy(x => x.Value).Select(x => x.Value).ToList();
        Assert.Equal(expectedAsc, sorted.Select(x => x.Value).ToList());

        // Now test descending by Value
        sorted = queryable.ApplySorting(sortDesc.ToSortDescriptors(), mapper).ToList();
        var expectedDesc = data.OrderByDescending(x => x.Value).Select(x => x.Value).ToList();
        Assert.Equal(expectedDesc, sorted.Select(x => x.Value).ToList());
    }
}