namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;

internal interface IPropertyMapper<TEntity, TModel>
{
    Expression<Func<TEntity, object>> ToEntityPropertyExpression(string propertyName);
}