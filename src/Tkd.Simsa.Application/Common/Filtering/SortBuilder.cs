namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Extensions;

public static class Sort
{
    public static ISortBuilder<T> By<T>(Expression<Func<T, object>> propertyExpression, SortDirection direction = SortDirection.Ascending)
        => new SortBuilder<T>().AddNewSortDescriptor(propertyExpression, direction);

    private class SortBuilder<T> : ISortBuilder<T>
    {
        private readonly SortDescriptors<T> sorts = [];

        public SortBuilder<T> AddNewSortDescriptor(Expression<Func<T, object>> propertyExpression, SortDirection direction)
        {
            var propertyName = propertyExpression.GetPropertyPath();
            this.sorts.Add(new SortDescriptor<T>(propertyName, direction));
            return this;
        }

        public ISortBuilder<T> ThenBy(Expression<Func<T, object>> propertyExpression, SortDirection direction = SortDirection.Ascending)
            => this.AddNewSortDescriptor(propertyExpression, direction);

        public SortDescriptors<T> ToSortDescriptors()
            => this.sorts;
    }
}

public interface ISortBuilder<T>
{
    ISortBuilder<T> ThenBy(Expression<Func<T, object>> propertyExpression, SortDirection direction = SortDirection.Ascending);

    SortDescriptors<T> ToSortDescriptors();
}