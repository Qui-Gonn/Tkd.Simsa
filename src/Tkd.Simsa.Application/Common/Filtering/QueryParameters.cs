namespace Tkd.Simsa.Application.Common.Filtering;

public static class QueryParameters
{
    public static QueryParameters<TItem> Empty<TItem>() => QueryParameters<TItem>.Empty;
}

public sealed record QueryParameters<TItem>
{
    public static readonly QueryParameters<TItem> Empty = new ();

    public FilterDescriptors<TItem> Filters { get; init; } = [];

    public void AddFilter(FilterDescriptor<TItem> filterDescriptor)
    {
        this.Filters.Add(filterDescriptor);
    }
}