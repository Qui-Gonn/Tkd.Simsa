namespace Tkd.Simsa.Application.Common.Filtering;

public record SortDescriptor<TItem>(string PropertyName, SortDirection Direction = SortDirection.Ascending);