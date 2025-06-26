namespace Tkd.Simsa.Application.Common.Filtering;

public static class QueryParameters
{
    public static IQueryParametersBuilder<TItem> Create<TItem>()
        => new QueryParametersBuilder<TItem>();

    private class QueryParametersBuilder<T> : IQueryParametersBuilder<T>
    {
        private FilterDescriptors<T> filters = FilterDescriptors<T>.Empty;

        private PagingParameters paging = PagingParameters.NoPaging;

        private SortDescriptors<T> sortDescriptors = SortDescriptors<T>.Empty;

        public QueryParameters<T> Build()
            => new ()
            {
                Filters = this.filters,
                Sorts = this.sortDescriptors,
                Paging = this.paging,
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

        public IQueryParametersBuilder<T> WithPaging(int pageNumber, int pageSize)
        {
            this.paging = new PagingParameters(pageNumber, pageSize);
            return this;
        }

        public IQueryParametersBuilder<T> WithPaging(PagingParameters paging)
        {
            this.paging = paging;
            return this;
        }

        public IQueryParametersBuilder<T> WithSort(SortDescriptors<T> sortDescriptors)
        {
            this.sortDescriptors = sortDescriptors;
            return this;
        }

        public IQueryParametersBuilder<T> WithSort(SortDescriptor<T> sortDescriptor)
        {
            this.sortDescriptors = [sortDescriptor];
            return this;
        }
    }
}

public interface IQueryParametersBuilder<T>
{
    QueryParameters<T> Build();

    IQueryParametersBuilder<T> WithFilter(FilterDescriptors<T> filterDescriptors);

    IQueryParametersBuilder<T> WithFilter(FilterDescriptor<T> filterDescriptor);

    IQueryParametersBuilder<T> WithPaging(int pageNumber, int pageSize);

    IQueryParametersBuilder<T> WithPaging(PagingParameters paging);

    IQueryParametersBuilder<T> WithSort(SortDescriptors<T> sortDescriptors);

    IQueryParametersBuilder<T> WithSort(SortDescriptor<T> sortDescriptor);
}