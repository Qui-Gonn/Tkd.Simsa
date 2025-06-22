namespace Tkd.Simsa.Application.Common;

using MediatR;

using Tkd.Simsa.Application.Common.Filtering;

public record GetItemsQuery<TItem>(QueryParameters<TItem> QueryParameters) : IRequest<IEnumerable<TItem>>;

public record GetItemByIdQuery<TItem>(Guid Id) : IRequest<TItem?>;

public record AddItemCommand<TItem>(TItem Item) : IRequest<TItem?>;

public record UpdateItemCommand<TItem>(TItem Item) : IRequest<TItem?>;

public record DeleteItemCommand<TItem>(Guid Id) : IRequest;