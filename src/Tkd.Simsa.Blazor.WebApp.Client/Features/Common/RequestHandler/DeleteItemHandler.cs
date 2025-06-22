namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class DeleteItemHandler<TItem>(IGenericItemService<TItem> service)
    : IRequestHandler<DeleteItemCommand<TItem>>
{
    public async Task Handle(DeleteItemCommand<TItem> command, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(command.Id, cancellationToken);
    }
}