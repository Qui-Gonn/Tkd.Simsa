namespace Tkd.Simsa.Application.Extensions;

using System.Linq.Expressions;
using System.Reflection;

public static class ExpressionExtensions
{
    public static MemberExpression GetMemberExpression<TType, TProperty>(this Expression<Func<TType, TProperty>> propertyExpression)
    {
        var memberExpression = propertyExpression.Body is UnaryExpression unaryExpression
            ? (MemberExpression)unaryExpression.Operand
            : (MemberExpression)propertyExpression.Body;

        return memberExpression;
    }

    public static PropertyInfo GetPropertyInfo<TType, TProperty>(this Expression<Func<TType, TProperty>> propertyExpression)
        => (PropertyInfo)GetMemberExpression(propertyExpression).Member;

    public static string GetPropertyName<TType, TReturn>(this Expression<Func<TType, TReturn>> propertyExpression)
        => propertyExpression.GetPropertyInfo().Name;

    public static string GetPropertyPath<TType, TReturn>(this Expression<Func<TType, TReturn>> propertyExpression)
    {
        var path = new List<string>();
        Expression? expr = propertyExpression.Body;
        while (expr is MemberExpression member)
        {
            path.Insert(0, member.Member.Name);
            expr = member.Expression;
        }

        if (expr is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
        {
            path.Insert(0, unaryMember.Member.Name);
        }

        return string.Join(".", path);
    }
}