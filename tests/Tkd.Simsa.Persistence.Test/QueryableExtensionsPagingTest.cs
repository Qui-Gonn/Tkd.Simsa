namespace Tkd.Simsa.Persistence.Test;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Extensions;
using Tkd.Simsa.Persistence.Test.Helper;

public class QueryableExtensionsPagingTest
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    public void ApplyPaging_ReturnsCorrectPage(int pageNumber, int pageSize)
    {
        var data = new List<TestEntity>
        {
            new (Guid.NewGuid(), "A"),
            new (Guid.NewGuid(), "B"),
            new (Guid.NewGuid(), "C"),
            new (Guid.NewGuid(), "D"),
            new (Guid.NewGuid(), "E"),
        };
        var ordered = data.OrderBy(x => x.Value).AsQueryable().OrderBy(x => x.Value);
        var paging = new PagingParameters(pageNumber, pageSize);
        var paged = ordered.ApplyPaging(paging).ToList();
        var expected = ordered.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        Assert.Equal(expected.Select(x => x.Value), paged.Select(x => x.Value));
    }

    [Fact]
    public void ApplyPaging_WithNoPaging_ReturnsAll()
    {
        var data = new List<TestEntity>
        {
            new (Guid.NewGuid(), "A"),
            new (Guid.NewGuid(), "B"),
            new (Guid.NewGuid(), "C"),
        };
        var ordered = data.OrderBy(x => x.Value).AsQueryable().OrderBy(x => x.Value);
        var paged = ordered.ApplyPaging(PagingParameters.NoPaging).ToList();
        Assert.Equal(ordered.Select(x => x.Value), paged.Select(x => x.Value));
    }
}