namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class GetItemsHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<GetItemsQuery<TItem>, IEnumerable<TItem>>
{
    public async Task<IEnumerable<TItem>> Handle(GetItemsQuery<TItem> query, CancellationToken cancellationToken)
        => await service.GetItemsAsync(query.QueryParameters, cancellationToken);
}