namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public class CompositeFilterDescriptor<TModel> : FilterDescriptor<TModel>
{
    public CompositeFilterDescriptor(FilterDescriptor<TModel> left, FilterDescriptor<TModel> right, LogicalOperator logicalOperator)
    {
        this.Left = left;
        this.Right = right;
        this.LogicalOperator = logicalOperator;
    }

    public FilterDescriptor<TModel> Left { get; }

    public LogicalOperator LogicalOperator { get; }

    public FilterDescriptor<TModel> Right { get; }

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