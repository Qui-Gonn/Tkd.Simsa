namespace Tkd.Simsa.Application.Common;

using System.Linq.Expressions;

public static class QueryParameters
{
    public static QueryParameters<TItem> Empty<TItem>() => QueryParameters<TItem>.Empty;
}

public sealed record QueryParameters<TItem>
{
    public static readonly QueryParameters<TItem> Empty = new ();

    public List<IFilterDescriptor<TItem>> FilterDescriptors { get; init; } = [];
}

public record FilterDescriptor<TItem>(FilterOperator Operator, Expression<Func<TItem, object?>> Property, object? Value)
    : IFilterDescriptor<TItem>;

public interface IFilterDescriptor<TItem> : IFilterDescriptor<TItem, object>;

public interface IFilterDescriptor<TItem, TValue>
{
    public FilterOperator Operator { get; init; }

    public Expression<Func<TItem, TValue?>> Property { get; init; }

    public TValue? Value { get; init; }
}

public record FilterOperator(string OperatorName)
{
    public static readonly FilterOperator Contains = new (nameof(Contains));

    public static readonly FilterOperator EqualTo = new (nameof(EqualTo));

    public static readonly FilterOperator StartsWith = new (nameof(StartsWith));
}