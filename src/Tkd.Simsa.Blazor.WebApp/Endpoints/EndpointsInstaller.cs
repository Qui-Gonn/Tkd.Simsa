namespace Tkd.Simsa.Blazor.WebApp.Endpoints;

using Tkd.Simsa.Domain.Common;

public class EndpointsInstaller<TItem>
    where TItem : IHasId<Guid>
{
    private EndpointsInstaller(IEndpointRouteBuilder endpointRouteBuilder, string groupName)
    {
        this.EndpointRouteBuilder = endpointRouteBuilder;
        this.GroupName = groupName;
        this.EndpointsHandler = new EndpointsHandler<TItem>(this);
    }

    public string GroupName { get; }

    private IEndpointRouteBuilder EndpointRouteBuilder { get; }

    private EndpointsHandler<TItem> EndpointsHandler { get; }

    public static void MapDefaultGroup(IEndpointRouteBuilder builder, string groupName)
    {
        var endpointsInstaller = new EndpointsInstaller<TItem>(builder, groupName);
        endpointsInstaller.MapDefaultGroup();
    }

    private EndpointsInstaller<TItem> MapDefaultDelete(RouteGroupBuilder builder)
    {
        builder.MapDelete("/{id:guid}", this.EndpointsHandler.Delete)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultGet(RouteGroupBuilder builder)
    {
        builder.MapGet("/", this.EndpointsHandler.Get)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultGetById(RouteGroupBuilder builder)
    {
        builder.MapGet("/{id:guid}", this.EndpointsHandler.GetById)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private RouteGroupBuilder MapDefaultGroup()
    {
        var routeGroupBuilder = this.EndpointRouteBuilder.MapGroup(this.GroupName);
        this.MapDefaultGet(routeGroupBuilder)
            .MapDefaultGetById(routeGroupBuilder)
            .MapDefaultPost(routeGroupBuilder)
            .MapDefaultPut(routeGroupBuilder)
            .MapDefaultDelete(routeGroupBuilder);
        return routeGroupBuilder;
    }

    private EndpointsInstaller<TItem> MapDefaultPost(RouteGroupBuilder builder)
    {
        builder.MapPost("/", this.EndpointsHandler.Post)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultPut(RouteGroupBuilder builder)
    {
        builder.MapPut("/{id:guid}", this.EndpointsHandler.Put)
            .WithTags(typeof(TItem).Name);
        return this;
    }
}