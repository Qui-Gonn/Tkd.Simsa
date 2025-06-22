namespace Tkd.Simsa.Application.Extensions;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

public static class ExpressionExtensions
{
    public static MemberExpression GetMemberExpression<T>(this Expression<T> expression)
    {
        // Check if the body of the expression is a UnaryExpression
        if (expression.Body is UnaryExpression unaryExpression)
        {
            // If it is a UnaryExpression, we assume it is a conversion
            if (unaryExpression.Operand is MemberExpression memberExpression)
            {
                return memberExpression; // Return the underlying MemberExpression
            }
        }
        else if (expression.Body is MemberExpression memberExpression)
        {
            // If the body is directly a MemberExpression, return it
            return memberExpression;
        }

        // Throw an exception if no MemberExpression is found
        throw new InvalidOperationException("The provided expression does not contain a valid MemberExpression.");
    }

    public static bool TryGetMemberExpression<T>(this Expression<T> expression, [NotNullWhen(true)] out MemberExpression? memberExpression)
    {
        try
        {
            // Call the method to get the MemberExpression
            memberExpression = GetMemberExpression(expression);
            return true; // Return true if successful
        }
        catch (InvalidOperationException)
        {
            memberExpression = null; // Set output parameter to null if an exception is caught
            return false; // Return false to indicate failure
        }
    }
}