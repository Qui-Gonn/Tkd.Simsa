namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Extensions;

public static class FilterDescriptor
{
    public static IFilterDescriptor<TItem> Property<TItem>(Expression<Func<TItem, object>> propertyExpression, object? value, FilterOperator filterOperator)
        => new FilterDescriptor<TItem>(filterOperator, propertyExpression.GetPropertyName(), value);
}

public record FilterDescriptor<TItem>(FilterOperator Operator, string PropertyName, object? Value)
    : IFilterDescriptor<TItem>;