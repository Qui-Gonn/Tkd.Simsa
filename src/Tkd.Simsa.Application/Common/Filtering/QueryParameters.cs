namespace Tkd.Simsa.Application.Common.Filtering;

public sealed record QueryParameters<TItem>
{
    public static readonly QueryParameters<TItem> Empty = new ();

    public FilterDescriptors<TItem> Filters { get; init; } = FilterDescriptors<TItem>.Empty;

    public PagingParameters Paging { get; init; } = PagingParameters.NoPaging;

    public SortDescriptors<TItem> Sorts { get; init; } = SortDescriptors<TItem>.Empty;
}