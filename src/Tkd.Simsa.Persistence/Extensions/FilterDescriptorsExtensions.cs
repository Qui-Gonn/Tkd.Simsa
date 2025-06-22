namespace Tkd.Simsa.Persistence.Extensions;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Filtering;
using Tkd.Simsa.Persistence.Mapper;

internal static class FilterDescriptorsExtensions
{
    public static Expression<Func<TEntity, bool>>? BuildFilterExpression<TEntity, TModel>(
        this FilterDescriptors<TModel> filterDescriptors,
        IPropertyMapper<TEntity, TModel> propertyMapper)
    {
        foreach (var filterDescriptor in filterDescriptors)
        {
            return FilterHelper.GetFilterExpression(
                filterDescriptor.Operator,
                propertyMapper.ToEntityPropertyExpression(filterDescriptor.PropertyName),
                filterDescriptor.Value);
        }

        return null;
    }
}