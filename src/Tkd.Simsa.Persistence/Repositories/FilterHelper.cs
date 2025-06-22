namespace Tkd.Simsa.Persistence.Repositories;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Extensions;

internal static class FilterHelper
{
    public static bool TryGetFilterExpression<TModel>(
        IFilterDescriptor<TModel> filterDescriptor,
        [NotNullWhen(true)] out Expression<Func<TModel, bool>>? filterExpression)
    {
        filterExpression = null;

        // ParameterExpression parameterExp = Expression.Parameter(typeof(TModel), "x");
        Expression predicate = Expression.Constant(true); //x=>True && x.IsActive=true/false

        if (!filterDescriptor.Property.TryGetMemberExpression(out var memberExp))
        {
            return false;
        }

        if (filterDescriptor.Value is null)
        {
            return false;
        }

        ConstantExpression constantExp = Expression.Constant(filterDescriptor.Value);
        if (!TryGetCompareExpression(filterDescriptor.Operator, memberExp, constantExp, out var binaryExp))
        {
            return false;
        }

        // predicate = Expression.AndAlso(predicate, binaryExp);
        predicate = binaryExp;

        filterExpression = Expression.Lambda<Func<TModel, bool>>(predicate, filterDescriptor.Property.Parameters);
        return true;
    }

    private static bool TryGetCompareExpression(
        FilterOperator filterDescriptorOperator,
        MemberExpression memberExpression,
        ConstantExpression constantExpression,
        [NotNullWhen(true)] out Expression? expression)
    {
        expression = null;
        if (filterDescriptorOperator == FilterOperator.Contains)
        {
            var methodInfo = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), [typeof(DbFunctions), typeof(string), typeof(string)]);
            if (methodInfo is null)
            {
                return false;
            }

            expression = Expression.Call(methodInfo, Expression.Constant(EF.Functions), memberExpression, constantExpression);
            return true;
        }

        if (filterDescriptorOperator == FilterOperator.EqualTo)
        {
            expression = Expression.Equal(memberExpression, constantExpression);
            return true;
        }

        if (filterDescriptorOperator == FilterOperator.StartsWith)
        {
            return false;
        }

        return false;
    }
}