namespace Tkd.Simsa.Persistence.Test;

using Bogus;

using FluentAssertions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Test.Helper;

public class GenericRepositoryTest
{
    private const int TotalItemCount = 10;

    private readonly InMemorySqliteDbHelper<TestDbContext> dbHelper;

    public GenericRepositoryTest()
    {
        this.dbHelper = InMemorySqliteDbHelper.Create<TestDbContext>();
    }

    [Fact]
    public async Task GetItems_WithContainsFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var filterValue = new Faker().PickRandom(originalItems).Value[2..4];
        var expectedItems = originalItems.Where(item => item.Value.Contains(filterValue)).ToList();
        var queryParameters = new QueryParameters<TestModel>
        {
            Filters =
            [
                FilterDescriptor.Property<TestModel>(i => i.Value, filterValue, FilterOperators.Contains)
            ]
        };
        var repository = this.CreateRepository();

        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItems_WithEqualsFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var filterValue = new Faker().PickRandom(originalItems).Value;
        var expectedItems = originalItems.Where(item => item.Value == filterValue).ToList();
        var queryParameters = new QueryParameters<TestModel>
        {
            Filters =
            [
                FilterDescriptor.Property<TestModel>(i => i.Value, filterValue, FilterOperators.EqualTo)
            ]
        };
        var repository = this.CreateRepository();

        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItems_WithoutFilter_ShouldReturnAllItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var repository = this.CreateRepository();

        var retrievedItems = await repository.GetItemsAsync(QueryParameters<TestModel>.Empty);
        var listOfRetrievedItems = retrievedItems.ToList();

        listOfRetrievedItems.Should().NotBeNull();
        listOfRetrievedItems.Count.Should().Be(TotalItemCount);
        listOfRetrievedItems.Should().BeEquivalentTo(originalItems);
    }

    [Fact]
    public async Task GetItems_WithStartsWithFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var filterValue = new Faker().PickRandom(originalItems).Value[..2];
        var expectedItems = originalItems.Where(item => item.Value.StartsWith(filterValue)).ToList();
        var queryParameters = new QueryParameters<TestModel>
        {
            Filters =
            [
                FilterDescriptor.Property<TestModel>(i => i.Value, filterValue, FilterOperators.StartsWith)
            ]
        };
        var repository = this.CreateRepository();

        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    private TestRepository CreateRepository()
        => new (this.dbHelper.CreateDbContext(), MapperHelper.SubstituteForTestEntityAndTestModel());
}