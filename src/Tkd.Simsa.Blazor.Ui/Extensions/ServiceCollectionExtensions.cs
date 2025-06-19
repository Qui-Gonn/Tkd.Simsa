namespace Tkd.Simsa.Blazor.Ui.Extensions;

using Microsoft.Extensions.DependencyInjection;

using MudBlazor.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSimsaUiServices(this IServiceCollection services)
    {
        services.AddMudServices();
        return services;
    }
}