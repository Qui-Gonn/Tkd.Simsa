namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class AddItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<AddItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(AddItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.AddAsync(command.Item, cancellationToken);
}