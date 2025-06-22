namespace Tkd.Simsa.Blazor.WebApp.Extensions;

using MediatR;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Blazor.WebApp.Features.RequestHandler;
using Tkd.Simsa.Domain.Common;

public static class ServiceCollectionExtensions
{
    public static void AddGenericServices<TItem>(this IServiceCollection services)
        where TItem : IHasId<Guid>
    {
        services.AddGenericMediatRServices<TItem>();
    }

    private static void AddGenericMediatRServices<TItem>(this IServiceCollection services)
        where TItem : IHasId<Guid>
    {
        services.AddTransient<IRequestHandler<GetItemsQuery<TItem>, IEnumerable<TItem>>, GetItemsHandler<TItem>>();
        services.AddTransient<IRequestHandler<GetItemByIdQuery<TItem>, TItem?>, GetItemByIdHandler<TItem>>();
        services.AddTransient<IRequestHandler<AddItemCommand<TItem>, TItem?>, AddItemHandler<TItem>>();
        services.AddTransient<IRequestHandler<UpdateItemCommand<TItem>, TItem?>, UpdateItemHandler<TItem>>();
        services.AddTransient<IRequestHandler<DeleteItemCommand<TItem>>, DeleteItemHandler<TItem>>();
    }
}