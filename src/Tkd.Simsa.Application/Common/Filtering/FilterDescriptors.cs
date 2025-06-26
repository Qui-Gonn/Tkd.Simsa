namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public class FilterDescriptors<TModel> : List<FilterDescriptor<TModel>>
{
    public static readonly FilterDescriptors<TModel> Empty = [];

    public Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions)
    {
        Expression predicate = Expression.Constant(true);
        var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
        foreach (var filterDescriptor in this)
        {
            var filterExpression = filterDescriptor.ToExpression(propertyMapper, comparisonFunctions);
            predicate = Expression.AndAlso(predicate, Expression.Invoke(filterExpression, parameterExpression));
        }

        return Expression.Lambda<Func<TEntity, bool>>(predicate, parameterExpression);
    }
}