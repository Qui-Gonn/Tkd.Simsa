namespace Tkd.Simsa.Application.Test;

using System.Text.Json;

using FluentAssertions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.PersonManagement;

public class QueryParametersTest
{
    [Fact]
    public void ShouldBeSerializable_WithFilter()
    {
        const string SearchValue = "er";
        var queryParameters = QueryParameters.Create<Person>()
                                             .WithFilter(
                                                 Filter.Or(
                                                     Filter.For<Person>().Property(i => i.Name.FirstName).Contains(SearchValue),
                                                     Filter.For<Person>().Property(i => i.Name.LastName).Contains(SearchValue)))
                                             .Build();

        var options = CreateOptions();
        var serializedQueryParameters = JsonSerializer.Serialize(queryParameters, options);
        var deserializedQueryParameters = JsonSerializer.Deserialize<QueryParameters<Person>>(serializedQueryParameters, options);

        deserializedQueryParameters.Should().NotBeNull();
        deserializedQueryParameters.Should().BeEquivalentTo(queryParameters);
    }

    [Fact]
    public void ShouldBeSerializable_WithFilterAndSort()
    {
        const string SearchValue = "er";
        var queryParameters = QueryParameters.Create<Person>()
                                             .WithFilter(
                                                 Filter.Or(
                                                     Filter.For<Person>().Property(i => i.Name.FirstName).Contains(SearchValue),
                                                     Filter.For<Person>().Property(i => i.Name.LastName).Contains(SearchValue)))
                                             .WithSort(Sort.By<Person>(i => i.Name.LastName).ThenBy(i => i.Name.FirstName))
                                             .Build();

        var options = CreateOptions();
        var serializedQueryParameters = JsonSerializer.Serialize(queryParameters, options);
        var deserializedQueryParameters = JsonSerializer.Deserialize<QueryParameters<Person>>(serializedQueryParameters, options);

        deserializedQueryParameters.Should().NotBeNull();
        deserializedQueryParameters.Should().BeEquivalentTo(queryParameters);
    }

    [Fact]
    public void ShouldBeSerializable_WithSort()
    {
        var queryParameters = QueryParameters.Create<Person>()
                                             .WithSort(Sort.By<Person>(i => i.Name.LastName).ThenBy(i => i.Name.FirstName))
                                             .Build();

        var options = CreateOptions();
        var serializedQueryParameters = JsonSerializer.Serialize(queryParameters, options);
        var deserializedQueryParameters = JsonSerializer.Deserialize<QueryParameters<Person>>(serializedQueryParameters, options);

        deserializedQueryParameters.Should().NotBeNull();
        deserializedQueryParameters.Should().BeEquivalentTo(queryParameters);
    }

    private static JsonSerializerOptions CreateOptions()
        => new ()
        {
            Converters = { new FilterDescriptorJsonConverterFactory() }
        };
}