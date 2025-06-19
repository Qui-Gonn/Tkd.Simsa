namespace Tkd.Simsa.Blazor.WebApp.Extensions;

using Tkd.Simsa.Blazor.WebApp.Endpoints;
using Tkd.Simsa.Domain.Common;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapDefaultEndpoints<T>(this IEndpointRouteBuilder builder)
        where T : IHasId<Guid>
    {
        EndpointsInstaller<T>.MapDefaultGroup(builder, typeof(T).Name);
        return builder;
    }
}