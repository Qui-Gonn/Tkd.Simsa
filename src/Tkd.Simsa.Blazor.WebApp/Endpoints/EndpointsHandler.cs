namespace Tkd.Simsa.Blazor.WebApp.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.Common;

public class EndpointsHandler<TItem>
    where TItem : IHasId<Guid>
{
    public EndpointsHandler(EndpointsInstaller<TItem> endpointsInstaller)
    {
        this.EndpointsInstaller = endpointsInstaller;
    }

    private EndpointsInstaller<TItem> EndpointsInstaller { get; }

    public async Task<NoContent> Delete(Guid id, IMediator mediator)
    {
        await mediator.Send(new DeleteItemCommand<TItem>(id));
        return TypedResults.NoContent();
    }

    public async Task<Ok<IEnumerable<TItem>>> Get(IMediator mediator)
        => TypedResults.Ok(await mediator.Send(new GetItemsQuery<TItem>(QueryParameters<TItem>.Empty)));

    public async Task<Results<Ok<TItem>, NotFound>> GetById(Guid id, IMediator mediator)
        => await mediator.Send(new GetItemByIdQuery<TItem>(id)) is { } itemById
            ? TypedResults.Ok(itemById)
            : TypedResults.NotFound();

    public async Task<Created<TItem>> Post(TItem itemToAdd, IMediator mediator)
    {
        var newItem = await mediator.Send(new AddItemCommand<TItem>(itemToAdd));
        return TypedResults.Created($"/api/{this.EndpointsInstaller.GroupName}/{newItem?.Id ?? itemToAdd.Id}", newItem);
    }

    public async Task<Ok<TItem>> Put(Guid id, TItem itemToUpdate, IMediator mediator)
        => TypedResults.Ok(await mediator.Send(new UpdateItemCommand<TItem>(itemToUpdate)));

    public async Task<Ok<IEnumerable<TItem>>> Query([FromBody] QueryParameters<TItem> queryParameters, IMediator mediator)
        => TypedResults.Ok(await mediator.Send(new GetItemsQuery<TItem>(queryParameters)));
}