namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Application.Extensions;

public record SimpleFilterDescriptorDescriptor<TModel>(string PropertyName, object? Value, FilterOperator FilterOperator)
    : FilterDescriptor<TModel>
{
    public override Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions)
    {
        var propertyExpression = PropertyExpressionBuilder.BuildPropertyExpression<TModel>(this.PropertyName);
        var entityPropertyExpression = propertyExpression.TranslateFromModelToEntity(propertyMapper);
        var filterExpression = comparisonFunctions.GetComparisonExpression(this.FilterOperator, entityPropertyExpression.GetMemberExpression(), this.Value);

        var lambdaFilterExpression = Expression.Lambda<Func<TEntity, bool>>(filterExpression, entityPropertyExpression.Parameters);
        return lambdaFilterExpression;
    }
}