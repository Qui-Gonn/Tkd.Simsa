namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class DeleteItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<DeleteItemCommand<TItem>>
{
    public async Task Handle(DeleteItemCommand<TItem> command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.Id, cancellationToken);
    }
}