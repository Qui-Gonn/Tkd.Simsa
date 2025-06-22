namespace Tkd.Simsa.Application.Common;

using Tkd.Simsa.Application.Common.Filtering;

public interface IGenericItemService<TItem>
{
    ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<TItem>> GetItemsAsync(QueryParameters<TItem> queryParameters, CancellationToken cancellationToken = default);

    ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default);
}