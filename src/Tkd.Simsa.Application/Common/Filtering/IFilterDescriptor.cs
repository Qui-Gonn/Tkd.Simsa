namespace Tkd.Simsa.Application.Common.Filtering;

public interface IFilterDescriptor<TItem, TValue>
{
    public FilterOperator Operator { get; init; }

    public string PropertyName { get; init; }

    public TValue? Value { get; init; }
}

public interface IFilterDescriptor<TItem> : IFilterDescriptor<TItem, object>;