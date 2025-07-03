namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public static class PropertyExpressionBuilder
{
    public static Expression<Func<T, object>> BuildPropertyExpression<T>(string propertyName)
    {
        var param = Expression.Parameter(typeof(T), "x");
        Expression body = param;
        foreach (var part in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, part);
        }

        var converted = Expression.Convert(body, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, param);
    }
}
