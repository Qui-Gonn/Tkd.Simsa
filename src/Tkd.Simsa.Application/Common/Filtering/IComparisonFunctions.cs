namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public interface IComparisonFunctions
{
    Expression GetComparisonExpression<T>(FilterOperator filterOperator, MemberExpression propertyExpression, T? filterValue);

    Expression GetEqualsExpression<T>(MemberExpression propertyExpression, T? filterValue);

    Expression GetLikeExpression<T>(MemberExpression propertyExpression, T? filterValue);

    Expression GetNotEqualsExpression<T>(MemberExpression propertyExpression, T? filterValue);

    Expression GetStartsWithExpression<T>(MemberExpression propertyExpression, T? filterValue);
}