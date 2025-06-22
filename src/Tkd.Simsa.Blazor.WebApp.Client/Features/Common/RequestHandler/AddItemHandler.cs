namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class AddItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<AddItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(AddItemCommand<TItem> command, CancellationToken cancellationToken)
        => await service.AddAsync(command.Item, cancellationToken);
}