namespace Tkd.Simsa.Persistence.Extensions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.Common;
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

    public static IQueryable<TEntity> ApplyPaging<TEntity>(
        this IOrderedQueryable<TEntity> orderedQueryable,
        PagingParameters paging)
        where TEntity : IHasId<Guid>
    {
        if (paging is { PageNumber: > 0, PageSize: > 0 })
        {
            var skip = (paging.PageNumber - 1) * paging.PageSize;
            return orderedQueryable.Skip(skip).Take(paging.PageSize);
        }

        return orderedQueryable;
    }

    public static IOrderedQueryable<TEntity> ApplySorting<TEntity, TModel>(
        this IQueryable<TEntity> queryable,
        SortDescriptors<TModel> sortDescriptors,
        IPropertyMapper<TEntity, TModel> propertyMapper)
        where TEntity : IHasId<Guid>
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

        return ordered ?? queryable.OrderBy(i => i.Id);
    }
}