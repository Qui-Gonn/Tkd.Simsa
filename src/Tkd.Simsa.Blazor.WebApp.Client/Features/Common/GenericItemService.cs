namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common;

using Microsoft.Extensions.Options;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.Common;

internal class GenericItemService<TItem> : IGenericItemService<TItem>
    where TItem : IHasId<Guid>
{
    private readonly ApiHttpClient apiClient;

    private readonly EndpointConfig<TItem> endpointConfig;

    public GenericItemService(ApiHttpClient apiClient, IOptions<EndpointConfig<TItem>> options)
    {
        this.apiClient = apiClient;
        this.endpointConfig = options.Value;
    }

    private string Endpoint => this.endpointConfig.ApiEndpoint;

    private string QueryEndpoint => $"{this.endpointConfig.ApiEndpoint}/query";

    public async ValueTask<TItem?> AddAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PostAsync<TItem, TItem>(this.Endpoint, item, cancellationToken);

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.DeleteAsync(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<TItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.apiClient.GetAsync<TItem>(this.BuildIdEndpoint(id), cancellationToken);

    public async ValueTask<IEnumerable<TItem>> GetItemsAsync(QueryParameters<TItem> queryParameters, CancellationToken cancellationToken = default)
        => await this.apiClient.PostAsync<QueryParameters<TItem>, TItem[]>(this.QueryEndpoint, queryParameters, cancellationToken) ?? [];

    public async ValueTask<TItem?> UpdateAsync(TItem item, CancellationToken cancellationToken = default)
        => await this.apiClient.PutAsync<TItem, TItem>(this.BuildIdEndpoint(item.Id), item, cancellationToken);

    private string BuildIdEndpoint(Guid id) => string.Format($"{this.Endpoint}/{{0}}", id);
}