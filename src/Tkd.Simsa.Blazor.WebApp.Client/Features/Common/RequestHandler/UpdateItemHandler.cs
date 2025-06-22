namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class UpdateItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<UpdateItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(UpdateItemCommand<TItem> command, CancellationToken cancellationToken)
        => await service.UpdateAsync(command.Item, cancellationToken);
}