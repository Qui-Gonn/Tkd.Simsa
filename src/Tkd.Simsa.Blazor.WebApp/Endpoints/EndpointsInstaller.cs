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
        this.RouteGroupBuilder = this.EndpointRouteBuilder.MapGroup(this.GroupName);
    }

    public string GroupName { get; }

    public RouteGroupBuilder RouteGroupBuilder { get; }

    private IEndpointRouteBuilder EndpointRouteBuilder { get; }

    private EndpointsHandler<TItem> EndpointsHandler { get; }

    public static EndpointsInstaller<TItem> MapDefaultGroup(IEndpointRouteBuilder builder, string groupName)
    {
        var endpointsInstaller = new EndpointsInstaller<TItem>(builder, groupName);
        endpointsInstaller.MapDefaultEndpoints();
        return endpointsInstaller;
    }

    private EndpointsInstaller<TItem> MapDefaultDelete()
    {
        this.RouteGroupBuilder.MapDelete("/{id:guid}", this.EndpointsHandler.Delete)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private RouteGroupBuilder MapDefaultEndpoints()
    {
        this.MapDefaultGet()
            .MapDefaultQuery()
            .MapDefaultGetById()
            .MapDefaultPost()
            .MapDefaultPut()
            .MapDefaultDelete();
        return this.RouteGroupBuilder;
    }

    private EndpointsInstaller<TItem> MapDefaultGet()
    {
        this.RouteGroupBuilder.Map("/", this.EndpointsHandler.Get)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultGetById()
    {
        this.RouteGroupBuilder.MapGet("/{id:guid}", this.EndpointsHandler.GetById)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultPost()
    {
        this.RouteGroupBuilder.MapPost("/", this.EndpointsHandler.Post)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultPut()
    {
        this.RouteGroupBuilder.MapPut("/{id:guid}", this.EndpointsHandler.Put)
            .WithTags(typeof(TItem).Name);
        return this;
    }

    private EndpointsInstaller<TItem> MapDefaultQuery()
    {
        this.RouteGroupBuilder.MapPost("/query", this.EndpointsHandler.Query)
            .WithTags(typeof(TItem).Name);
        return this;
    }
}