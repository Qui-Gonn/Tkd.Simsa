namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public record CompositeFilterDescriptor<TModel>(FilterDescriptor<TModel> Left, FilterDescriptor<TModel> Right, LogicalOperator LogicalOperator)
    : FilterDescriptor<TModel>
{
    public override Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions)
    {
        var leftExpression = this.Left.ToExpression(propertyMapper, comparisonFunctions);
        var rightExpression = this.Right.ToExpression(propertyMapper, comparisonFunctions);

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var leftBody = Expression.Invoke(leftExpression, parameter);
        var rightBody = Expression.Invoke(rightExpression, parameter);

        Expression combinedExpression = this.LogicalOperator switch
        {
            LogicalOperator.And => Expression.AndAlso(leftBody, rightBody),
            LogicalOperator.Or => Expression.OrElse(leftBody, rightBody),
            _ => throw new NotSupportedException($"Logical operator {this.LogicalOperator} is not supported.")
        };

        return Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);
    }
}