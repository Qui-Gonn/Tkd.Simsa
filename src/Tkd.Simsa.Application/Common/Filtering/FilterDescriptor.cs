namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public abstract class FilterDescriptor<TModel>
{
    public static SimpleFilterDescriptorDescriptor<TModel> ForProperty(
        Expression<Func<TModel, object>> propertyExpression,
        object? value,
        FilterOperator filterOperator) =>
        new (propertyExpression, value, filterOperator);

    public abstract Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions);
}