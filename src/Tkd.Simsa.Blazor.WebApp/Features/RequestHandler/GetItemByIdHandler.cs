namespace Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;

using MediatR;

using Tkd.Simsa.Application.Common;

internal class GetItemByIdHandler<TItem>(IGenericRepository<TItem> repository)
    : IRequestHandler<GetItemByIdQuery<TItem>, TItem?>
{
    public async Task<TItem?> Handle(GetItemByIdQuery<TItem> query, CancellationToken cancellationToken)
        => await repository.GetByIdAsync(query.Id, cancellationToken);
}