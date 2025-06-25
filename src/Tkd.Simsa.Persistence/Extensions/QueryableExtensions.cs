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

    public static IQueryable<TEntity> ApplySorting<TEntity, TModel>(
        this IQueryable<TEntity> queryable,
        SortDescriptors<TModel> sortDescriptors,
        IPropertyMapper<TEntity, TModel> propertyMapper)
    {
        IOrderedQueryable<TEntity>? ordered = null;
        foreach (var sortDescriptor in sortDescriptors)
        {
            var property = sortDescriptor.PropertyExpression.TranslateFromModelToEntity(propertyMapper);
            if (ordered is null)
            {
                ordered = sortDescriptor.Direction == SortDirection.Ascending
                    ? queryable.OrderBy(property)
                    : queryable.OrderByDescending(property);
            }
            else
            {
                ordered = sortDescriptor.Direction == SortDirection.Ascending
                    ? ordered.ThenBy(property)
                    : ordered.ThenByDescending(property);
            }
        }

        return ordered ?? queryable;
    }
}