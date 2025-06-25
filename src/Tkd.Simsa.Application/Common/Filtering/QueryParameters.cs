namespace Tkd.Simsa.Application.Common.Filtering;

public sealed record QueryParameters<TItem>
{
    public static readonly QueryParameters<TItem> Empty = new ();

    public FilterDescriptors<TItem> Filters { get; init; } = [];

    public SortDescriptors<TItem> SortDescriptors { get; init; } = [];
}