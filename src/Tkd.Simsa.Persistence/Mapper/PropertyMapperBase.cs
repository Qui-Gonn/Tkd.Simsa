namespace Tkd.Simsa.Persistence.Mapper;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Common.Filtering;

public abstract class PropertyMapperBase<TEntity, TModel> : IPropertyMapper<TEntity, TModel>
{
    protected abstract Dictionary<string, Expression<Func<TEntity, object>>> PropertyMap { get; }

    public Expression<Func<TEntity, object>> ToEntityPropertyExpression(string propertyName)
        => this.PropertyMap.TryGetValue(propertyName, out var expression)
            ? expression
            : throw new NotSupportedException(propertyName);
}