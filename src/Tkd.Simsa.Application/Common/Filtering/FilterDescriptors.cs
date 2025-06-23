namespace Tkd.Simsa.Application.Common.Filtering;

using System.Collections;
using System.Linq.Expressions;

public class FilterDescriptors<TModel> : ICollection<FilterDescriptor<TModel>>
{
    private readonly List<FilterDescriptor<TModel>> filters = [];

    public int Count => this.filters.Count;

    public bool IsReadOnly => false;

    public void Add(FilterDescriptor<TModel> item)
    {
        this.filters.Add(item);
    }

    public void Clear()
    {
        this.filters.Clear();
    }

    public bool Contains(FilterDescriptor<TModel> item)
    {
        return this.filters.Contains(item);
    }

    public void CopyTo(FilterDescriptor<TModel>[] array, int arrayIndex)
    {
        this.filters.CopyTo(array, arrayIndex);
    }

    public IEnumerator<FilterDescriptor<TModel>> GetEnumerator()
    {
        return this.filters.GetEnumerator();
    }

    public bool Remove(FilterDescriptor<TModel> item)
    {
        return this.filters.Remove(item);
    }

    public Expression<Func<TEntity, bool>> ToExpression<TEntity>(
        IPropertyMapper<TEntity, TModel> propertyMapper,
        IComparisonFunctions comparisonFunctions)
    {
        Expression predicate = Expression.Constant(true);
        var parameterExpression = Expression.Parameter(typeof(TEntity), "x");
        foreach (var filterDescriptor in this.filters)
        {
            var filterExpression = filterDescriptor.ToExpression(propertyMapper, comparisonFunctions);
            predicate = Expression.AndAlso(predicate, Expression.Invoke(filterExpression, parameterExpression));
        }

        return Expression.Lambda<Func<TEntity, bool>>(predicate, parameterExpression);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)this.filters).GetEnumerator();
    }
}