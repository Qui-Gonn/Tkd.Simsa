namespace Tkd.Simsa.Persistence.Extensions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Filtering;

internal static class QueryableExtensions
{
    public static IQueryable<TEntity> ApplyFilters<TEntity, TModel>(
        this IQueryable<TEntity> queryable,
        FilterDescriptors<TModel> filterDescriptors,
        IPropertyMapper<TEntity, TModel> propertyMapper)
    {
        return filterDescriptors.ToExpression(propertyMapper, ComparisonFunctions.Instance) is { } filterExpression
            ? queryable.Where(filterExpression)
            : queryable;
    }
}