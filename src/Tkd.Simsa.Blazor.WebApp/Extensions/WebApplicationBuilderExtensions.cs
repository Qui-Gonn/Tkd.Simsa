namespace Tkd.Simsa.Blazor.WebApp.Extensions;

using Tkd.Simsa.Domain.EventManagement;
using Tkd.Simsa.Domain.PersonManagement;
using Tkd.Simsa.Persistence.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSimsaWebAppServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());

        builder.Services.AddGenericServices<Event>();
        builder.Services.AddGenericServices<Person>();

        var connectionString = builder.Configuration.GetConnectionString("SimsaDb") ?? string.Empty;
        builder.Services.AddSimsaPersistenceServices(connectionString);

        return builder;
    }
}