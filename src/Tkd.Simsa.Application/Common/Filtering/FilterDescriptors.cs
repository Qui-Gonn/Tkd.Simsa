namespace Tkd.Simsa.Application.Common.Filtering;

using System.Collections;

public class FilterDescriptors<TItem> : ICollection<IFilterDescriptor<TItem>>
{
    private readonly ICollection<IFilterDescriptor<TItem>> filters = [];

    public int Count => this.filters.Count;

    public bool IsReadOnly => this.filters.IsReadOnly;

    public void Add(IFilterDescriptor<TItem> item)
    {
        this.filters.Add(item);
    }

    public void Clear()
    {
        this.filters.Clear();
    }

    public bool Contains(IFilterDescriptor<TItem> item)
    {
        return this.filters.Contains(item);
    }

    public void CopyTo(IFilterDescriptor<TItem>[] array, int arrayIndex)
    {
        this.filters.CopyTo(array, arrayIndex);
    }

    public IEnumerator<IFilterDescriptor<TItem>> GetEnumerator()
    {
        return this.filters.GetEnumerator();
    }

    public bool Remove(IFilterDescriptor<TItem> item)
    {
        return this.filters.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)this.filters).GetEnumerator();
    }
}