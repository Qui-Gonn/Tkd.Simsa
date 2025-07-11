namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public interface IPropertyMapper<TEntity, TModel>
{
    Expression<Func<TEntity, object>> ToEntityPropertyExpression(string propertyName);
}