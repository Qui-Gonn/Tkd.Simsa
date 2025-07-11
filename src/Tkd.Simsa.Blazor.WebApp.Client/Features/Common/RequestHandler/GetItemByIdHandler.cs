namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class GetItemByIdHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<GetItemByIdQuery<TItem>, TItem?>
{
    public async Task<TItem?> Handle(GetItemByIdQuery<TItem> query, CancellationToken cancellationToken)
        => await service.GetByIdAsync(query.Id, cancellationToken);
}