﻿namespace Tkd.Simsa.Blazor.WebApp.Client.Extensions;

using System.Text.Json;

using MediatR;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Blazor.WebApp.Client.Features.Common;
using Tkd.Simsa.Blazor.WebApp.Client.Features.Common.RequestHandler;
using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimsaWasmServices(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddHttpClient<ApiHttpClient>(client => client.BaseAddress = new Uri(hostEnvironment.BaseAddress));

        services.AddSimsaJsonOptions();
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

        services.AddGenericServices<Event>();
        services.AddGenericServices<Person>();

        return services;
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

    private static void AddGenericServices<TItem>(this IServiceCollection services)
        where TItem : IHasId<Guid>
    {
        services.RegisterApiClientService<TItem>();
        services.AddGenericMediatRServices<TItem>();
    }

    private static void AddSimsaJsonOptions(this IServiceCollection services)
    {
        var simsaJsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        simsaJsonOptions.Converters.Add(new FilterDescriptorJsonConverterFactory());
        services.AddSingleton(simsaJsonOptions);
    }

    private static void RegisterApiClientService<TItem>(this IServiceCollection services)
        where TItem : IHasId<Guid>
    {
        services.AddScoped<IGenericItemService<TItem>, GenericItemService<TItem>>();
        services.Configure<EndpointConfig<TItem>>(config => config.ApiEndpoint = $"api/{typeof(TItem).Name}");
    }
}