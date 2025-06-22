namespace Tkd.Simsa.Persistence.Filtering;

using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Application.Extensions;

internal static class FilterHelper
{
    private static readonly MethodInfo LikeFunctionMethodInfo;

    static FilterHelper()
    {
        LikeFunctionMethodInfo = typeof(DbFunctionsExtensions).GetMethod(
            nameof(DbFunctionsExtensions.Like),
            [typeof(DbFunctions), typeof(string), typeof(string)])!;
    }

    public static Expression<Func<TEntity, bool>> GetFilterExpression<TEntity>(
        FilterOperator filterOperator,
        Expression<Func<TEntity, object>> property,
        object? filterValue)
    {
        var propertyExpression = property.GetMemberExpression();

        var predicate = GetComparisonExpression(filterOperator, propertyExpression, filterValue);
        return Expression.Lambda<Func<TEntity, bool>>(predicate, property.Parameters);
    }

    private static Expression GetComparisonExpression(
        FilterOperator filterOperator,
        MemberExpression propertyExpression,
        object? filterValue)
    {
        if (filterOperator == FilterOperators.Contains)
        {
            var filterValueExpression = Expression.Constant($"%{filterValue}%");
            return Expression.Call(LikeFunctionMethodInfo, Expression.Constant(EF.Functions), propertyExpression, filterValueExpression);
        }

        if (filterOperator == FilterOperators.EqualTo)
        {
            var filterValueExpression = Expression.Constant(filterValue);
            return Expression.Equal(propertyExpression, filterValueExpression);
        }

        if (filterOperator == FilterOperators.StartsWith)
        {
            var filterValueExpression = Expression.Constant($"{filterValue}%");
            return Expression.Call(LikeFunctionMethodInfo, Expression.Constant(EF.Functions), propertyExpression, filterValueExpression);
        }

        throw new NotSupportedException(filterOperator.ToString());
    }
}