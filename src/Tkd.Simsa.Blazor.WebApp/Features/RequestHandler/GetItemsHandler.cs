namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class GetItemsHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetItemsQuery<TItem>, IEnumerable<TItem>>
{
    public async Task<IEnumerable<TItem>> Handle(GetItemsQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetItemsAsync(query.QueryParameters, cancellationToken);
}