namespace Tkd.Simsa.Application.Common.Filtering;

using System.Linq.Expressions;

public static class Filter
{
    public static CompositeFilterDescriptorDescriptor<T> And<T>(FilterDescriptor<T> left, FilterDescriptor<T> right)
        => new FilterBuilder<T>().And(left, right);

    public static IFilterBuilder<T> For<T>()
        => new FilterBuilder<T>();

    public static CompositeFilterDescriptorDescriptor<T> Or<T>(FilterDescriptor<T> left, FilterDescriptor<T> right)
        => new FilterBuilder<T>().Or(left, right);

    private class FilterBuilder<T> : IFilterBuilder<T>, IFilterBuilderSimple<T>
    {
        private Expression<Func<T, object>> propertyExpression = null!;

        public CompositeFilterDescriptorDescriptor<T> And(FilterDescriptor<T> left, FilterDescriptor<T> right)
            => new (left, right, LogicalOperator.And);

        public SimpleFilterDescriptorDescriptor<T> Contains(object value)
            => new (this.propertyExpression, value, FilterOperator.Contains);

        public SimpleFilterDescriptorDescriptor<T> EqualTo(object value)
            => new (this.propertyExpression, value, FilterOperator.Equals);

        public CompositeFilterDescriptorDescriptor<T> Or(FilterDescriptor<T> left, FilterDescriptor<T> right)
            => new (left, right, LogicalOperator.Or);

        public IFilterBuilderSimple<T> Property(Expression<Func<T, object>> propertyExpression)
        {
            this.propertyExpression = propertyExpression;
            return this;
        }

        public SimpleFilterDescriptorDescriptor<T> StartsWith(object value)
            => new (this.propertyExpression, value, FilterOperator.StartsWith);
    }
}

public interface IFilterBuilder<T>
{
    CompositeFilterDescriptorDescriptor<T> And(FilterDescriptor<T> left, FilterDescriptor<T> right);

    CompositeFilterDescriptorDescriptor<T> Or(FilterDescriptor<T> left, FilterDescriptor<T> right);

    IFilterBuilderSimple<T> Property(Expression<Func<T, object>> propertyExpression);
}

public interface IFilterBuilderSimple<T>
{
    SimpleFilterDescriptorDescriptor<T> Contains(object value);

    SimpleFilterDescriptorDescriptor<T> EqualTo(object value);

    SimpleFilterDescriptorDescriptor<T> StartsWith(object value);
}