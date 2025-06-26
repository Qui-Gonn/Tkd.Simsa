namespace Tkd.Simsa.Persistence.Test;

using Bogus;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Test.Helper;

using Xunit.Abstractions;

public class GenericRepositoryTest
{
    private const int TotalItemCount = 10;

    private readonly InMemorySqliteDbHelper<TestDbContext> dbHelper;

    private readonly Faker faker = new ();

    private readonly ITestOutputHelper output;

    public GenericRepositoryTest(ITestOutputHelper output)
    {
        this.output = output;
        this.dbHelper = InMemorySqliteDbHelper.Create<TestDbContext>();
    }

    [Fact]
    public async Task GetItems_WithAndFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var randomItem = this.faker.PickRandom(originalItems);
        var filterValue1 = randomItem.Value[Math.Min(2, randomItem.Value.Length)..Math.Min(4, randomItem.Value.Length)];
        var filterValue2 = randomItem.Id;
        var expectedItems = this.dbHelper.CreateDbContext().TestEntities
                                .Where(item => EF.Functions.Like(item.Value, $"%{filterValue1}%")
                                               && item.Id == filterValue2).ToList();
        var repository = this.CreateRepository();

        var queryParameters = QueryParameters.Create<TestModel>()
                                             .WithFilter(
                                                 Filter.And(
                                                     Filter.For<TestModel>().Property(i => i.Value).Contains(filterValue1),
                                                     Filter.For<TestModel>().Property(i => i.Id).EqualTo(filterValue2)))
                                             .Build();
        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItems_WithContainsFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var randomItem = this.faker.PickRandom(originalItems);
        var filterValue = randomItem.Value[Math.Min(2, randomItem.Value.Length)..Math.Min(4, randomItem.Value.Length)];
        var expectedItems = this.dbHelper.CreateDbContext().TestEntities.Where(item => EF.Functions.Like(item.Value, $"%{filterValue}%")).ToList();
        var repository = this.CreateRepository();

        var queryParameters = QueryParameters.Create<TestModel>()
                                             .WithFilter(Filter.For<TestModel>().Property(i => i.Value).Contains(filterValue))
                                             .Build();
        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItems_WithEqualsFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var filterValue = this.PickRandomItem(originalItems);
        var expectedItems = this.dbHelper.CreateDbContext().TestEntities.Where(item => item.Value == filterValue).ToList();
        var repository = this.CreateRepository();

        var queryParameters = QueryParameters.Create<TestModel>()
                                             .WithFilter(Filter.For<TestModel>().Property(i => i.Value).EqualTo(filterValue))
                                             .Build();
        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    [Fact]
    public async Task GetItems_WithOrFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var randomItem1 = this.faker.PickRandom(originalItems);
        var randomItem2 = this.faker.PickRandom(originalItems);
        var filterValue1 = randomItem1.Value[Math.Min(2, randomItem1.Value.Length)..Math.Min(4, randomItem1.Value.Length)];
        var filterValue2 = randomItem2.Id;
        var expectedItems = this.dbHelper.CreateDbContext().TestEntities
                                .Where(item => EF.Functions.Like(item.Value, $"%{filterValue1}%")
                                               || item.Id == filterValue2).ToList();
        var repository = this.CreateRepository();

        var queryParameters = QueryParameters.Create<TestModel>()
                                             .WithFilter(
                                                 Filter.Or(
                                                     Filter.For<TestModel>().Property(i => i.Value).Contains(filterValue1),
                                                     Filter.For<TestModel>().Property(i => i.Id).EqualTo(filterValue2)))
                                             .Build();
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

        var retrievedItems = (await repository.GetItemsAsync(QueryParameters<TestModel>.Empty)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(TotalItemCount);
        retrievedItems.Should().BeEquivalentTo(originalItems);
    }

    [Fact]
    public async Task GetItems_WithStartsWithFilter_ShouldReturnOnlyMatchingItems()
    {
        var originalItems = this.dbHelper.GenerateFakeData<TestEntity>(TotalItemCount);
        var randomItem = this.faker.PickRandom(originalItems);
        var filterValue = randomItem.Value[..Math.Min(2, randomItem.Value.Length)];
        var expectedItems = this.dbHelper.CreateDbContext().TestEntities.Where(item => EF.Functions.Like(item.Value, $"{filterValue}%")).ToList();
        var repository = this.CreateRepository();

        var queryParameters = QueryParameters.Create<TestModel>()
                                             .WithFilter(Filter.For<TestModel>().Property(i => i.Value).StartsWith(filterValue))
                                             .Build();
        var retrievedItems = (await repository.GetItemsAsync(queryParameters)).ToList();

        retrievedItems.Should().NotBeNull();
        retrievedItems.Count.Should().Be(expectedItems.Count);
        retrievedItems.Should().BeEquivalentTo(expectedItems);
    }

    private TestRepository CreateRepository()
        => new (this.dbHelper.CreateDbContext(), MapperHelper.TestMapper);

    private string PickRandomItem(List<TestEntity> originalItems)
    {
        return this.faker.PickRandom(originalItems).Value;
    }
}