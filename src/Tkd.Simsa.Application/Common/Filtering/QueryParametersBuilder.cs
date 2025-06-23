namespace Tkd.Simsa.Application.Common.Filtering;

public static class QueryParameters
{
    public static IQueryParametersBuilder<TItem> Create<TItem>()
        => new QueryParametersBuilder<TItem>();

    public static QueryParameters<TItem> Empty<TItem>()
        => QueryParameters<TItem>.Empty;

    private class QueryParametersBuilder<T> : IQueryParametersBuilder<T>
    {
        private FilterDescriptors<T> filters = [];

        public QueryParameters<T> Build()
            => new ()
            {
                Filters = this.filters,
            };

        public IQueryParametersBuilder<T> WithFilter(FilterDescriptors<T> filterDescriptors)
        {
            this.filters = filterDescriptors;
            return this;
        }

        public IQueryParametersBuilder<T> WithFilter(FilterDescriptor<T> filterDescriptor)
        {
            this.filters = [filterDescriptor];
            return this;
        }
    }
}

public interface IQueryParametersBuilder<T>
{
    QueryParameters<T> Build();

    IQueryParametersBuilder<T> WithFilter(FilterDescriptors<T> filterDescriptors);

    IQueryParametersBuilder<T> WithFilter(FilterDescriptor<T> filterDescriptor);
}