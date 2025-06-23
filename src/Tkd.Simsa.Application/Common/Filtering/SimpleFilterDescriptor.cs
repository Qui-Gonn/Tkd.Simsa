namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Extensions;

public class SimpleFilterDescriptorDescriptor<TModel> : FilterDescriptor<TModel>
{
    public SimpleFilterDescriptorDescriptor(Expression<Func<TModel, object>> propertyExpression, object? value, FilterOperator filterOperator)
    {
        this.PropertyExpression = propertyExpression;
        this.Value = value;
        this.Operator = filterOperator;
    }

    public FilterOperator Operator { get; }

    public Expression<Func<TModel, object>> PropertyExpression { get; }

    public object? Value { get; }

    public override Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions)
    {
        var converter = new ExpressionConverter<TEntity, TModel>(propertyMapper);
        var propertyExpression = converter.Convert(this.PropertyExpression);
        var filterExpression = comparisonFunctions.GetComparisonExpression(this.Operator, propertyExpression.GetMemberExpression(), this.Value);

        var lambdaFilterExpression = Expression.Lambda<Func<TEntity, bool>>(filterExpression, propertyExpression.Parameters);
        return lambdaFilterExpression;
    }
}