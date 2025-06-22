namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common;

using System.Net.Http.Json;

public class ApiHttpClient
{
    private readonly HttpClient httpClient;

    public ApiHttpClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.DeleteAsync(endpoint, cancellationToken);
        return httpResponseMessage?.IsSuccessStatusCode ?? false;
    }

    public async Task<TResult?> GetAsync<TResult>(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.GetAsync(endpoint, cancellationToken);
        return await ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    public async Task<TResult?> PostAsync<TContent, TResult>(string endpoint, TContent content, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PostAsync(endpoint, JsonContent.Create(content), cancellationToken);
        return await ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    public async Task<TResult?> PutAsync<TContent, TResult>(string endpoint, TContent content, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PutAsync(endpoint, JsonContent.Create(content), cancellationToken);
        return await ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    private static async Task<TResult?> ReturnObjectOrDefault<TResult>(HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        return httpResponseMessage is { IsSuccessStatusCode: true }
            ? await httpResponseMessage.Content.ReadFromJsonAsync<TResult>(cancellationToken: cancellationToken)
            : default;
    }
}