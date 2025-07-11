namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class UpdateItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<UpdateItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(UpdateItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.UpdateAsync(command.Item, cancellationToken);
}