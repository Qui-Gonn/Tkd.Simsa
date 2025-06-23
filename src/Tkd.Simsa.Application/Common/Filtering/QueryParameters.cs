namespace Tkd.Simsa.Application.Common.Filtering;

public sealed record QueryParameters<TItem>
{
    public static readonly QueryParameters<TItem> Empty = new ();

    public FilterDescriptors<TItem> Filters { get; init; } = [];

    public void AddFilter(FilterDescriptor<TItem> filterDescriptor)
    {
        this.Filters.Add(filterDescriptor);
    }
}