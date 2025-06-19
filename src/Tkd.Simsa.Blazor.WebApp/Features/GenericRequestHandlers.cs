namespace Tkd.Simsa.Blazor.WebApp.Features;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class GetAllItemsHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetAllItemsQuery<TItem>, IEnumerable<TItem>>
{
    public async Task<IEnumerable<TItem>> Handle(GetAllItemsQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetAllAsync(cancellationToken);
}

internal class GetItemByIdHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetItemByIdQuery<TItem>, TItem?>
{
    public async Task<TItem?> Handle(GetItemByIdQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetByIdAsync(query.Id, cancellationToken);
}

internal class AddItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<AddItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(AddItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.AddAsync(command.Item, cancellationToken);
}

internal class UpdateItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<UpdateItemCommand<TItem>, TItem?>
{
    public async Task<TItem?> Handle(UpdateItemCommand<TItem> command, CancellationToken cancellationToken)
        => await repository.UpdateAsync(command.Item, cancellationToken);
}

internal class DeleteItemHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<DeleteItemCommand<TItem>>
{
    public async Task Handle(DeleteItemCommand<TItem> command, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(command.Id, cancellationToken);
    }
}