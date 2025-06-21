namespace Tkd.Simsa.Persistence.Test;

using FluentAssertions;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Persistence.Test.Helper;

public class GenericRepositoryTest
{
    private readonly InMemorySqliteDbHelper<TestDbContext> dbHelper;

    public GenericRepositoryTest()
    {
        this.dbHelper = InMemorySqliteDbHelper.Create<TestDbContext>();
    }

    [Fact]
    public async Task GetItemsWithoutFilterShouldReturnAllItems()
    {
        const int TotalItemCount = 10;
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var repository = this.CreateRepository();

        var retrievedItems = await repository.GetItemsAsync(QueryParameters<TestModel>.Empty);
        var listOfRetrievedItems = retrievedItems.ToList();

        listOfRetrievedItems.Should().NotBeNull();
        listOfRetrievedItems.Count.Should().Be(TotalItemCount);
        listOfRetrievedItems.Should().BeEquivalentTo(originalItems);
    }

    private TestRepository CreateRepository()
        => new (this.dbHelper.CreateDbContext(), MapperHelper.SubstituteForTestEntityAndTestModel());
}