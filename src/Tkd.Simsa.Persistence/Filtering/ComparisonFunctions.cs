namespace Tkd.Simsa.Persistence.Filtering;

using System.Linq.Expressions;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Application.Common.Filtering;

internal class ComparisonFunctions : IComparisonFunctions
{
    public static readonly ComparisonFunctions Instance = new ();

    public readonly MethodInfo LikeFunctionMethodInfo;

    private ComparisonFunctions()
    {
        this.LikeFunctionMethodInfo = typeof(DbFunctionsExtensions).GetMethod(
            nameof(DbFunctionsExtensions.Like),
            [typeof(DbFunctions), typeof(string), typeof(string)])!;
    }

    public Expression GetComparisonExpression<T>(
        FilterOperator filterOperator,
        MemberExpression propertyExpression,
        T? filterValue)
    {
        return filterOperator switch
        {
            FilterOperator.Contains => this.GetLikeExpression(propertyExpression, filterValue),
            FilterOperator.Equals => this.GetEqualsExpression(propertyExpression, filterValue),
            FilterOperator.NotEqual => this.GetNotEqualsExpression(propertyExpression, filterValue),
            FilterOperator.StartsWith => this.GetStartsWithExpression(propertyExpression, filterValue),
            _ => throw new NotSupportedException(filterOperator.ToString())
        };
    }

    public Expression GetEqualsExpression<T>(MemberExpression propertyExpression, T? filterValue)
        => Expression.Equal(propertyExpression, Expression.Constant(filterValue));

    public Expression GetLikeExpression<T>(MemberExpression propertyExpression, T? filterValue)
        => Expression.Call(this.LikeFunctionMethodInfo, Expression.Constant(EF.Functions), propertyExpression, Expression.Constant($"%{filterValue}%"));

    public Expression GetNotEqualsExpression<T>(MemberExpression propertyExpression, T? filterValue)
        => Expression.NotEqual(propertyExpression, Expression.Constant(filterValue));

    public Expression GetStartsWithExpression<T>(MemberExpression propertyExpression, T? filterValue)
        => Expression.Call(this.LikeFunctionMethodInfo, Expression.Constant(EF.Functions), propertyExpression, Expression.Constant($"{filterValue}%"));
}