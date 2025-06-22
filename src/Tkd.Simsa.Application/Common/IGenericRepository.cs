namespace Tkd.Simsa.Application.Common;

using Tkd.Simsa.Application.Common.Filtering;

public interface IGenericRepository<TModel>
{
    ValueTask<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<IEnumerable<TModel>> GetItemsAsync(QueryParameters<TModel> queryParameters, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void SetTransactionMode(TransactionMode transactionMode);

    ValueTask<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default);
}