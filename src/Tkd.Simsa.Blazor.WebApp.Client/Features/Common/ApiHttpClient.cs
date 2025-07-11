namespace Tkd.Simsa.Blazor.WebApp.Client.Features.Common;

using System.Net.Http.Json;
using System.Text.Json;

public class ApiHttpClient
{
    private readonly HttpClient httpClient;

    private readonly JsonSerializerOptions jsonSerializerOptions;

    public ApiHttpClient(HttpClient httpClient, JsonSerializerOptions jsonSerializerOptions)
    {
        this.httpClient = httpClient;
        this.jsonSerializerOptions = jsonSerializerOptions;
    }

    public async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.DeleteAsync(endpoint, cancellationToken);
        return httpResponseMessage?.IsSuccessStatusCode ?? false;
    }

    public async Task<TResult?> GetAsync<TResult>(string endpoint, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.GetAsync(endpoint, cancellationToken);
        return await this.ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    public async Task<TResult?> PostAsync<TContent, TResult>(string endpoint, TContent content, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PostAsync(
            endpoint,
            JsonContent.Create(content, options: this.jsonSerializerOptions),
            cancellationToken);
        return await this.ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    public async Task<TResult?> PutAsync<TContent, TResult>(string endpoint, TContent content, CancellationToken cancellationToken = default)
    {
        var httpResponseMessage = await this.httpClient.PutAsync(
            endpoint,
            JsonContent.Create(
                content,
                options: this.jsonSerializerOptions),
            cancellationToken);
        return await this.ReturnObjectOrDefault<TResult>(httpResponseMessage, cancellationToken);
    }

    private async Task<TResult?> ReturnObjectOrDefault<TResult>(HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken = default)
    {
        return httpResponseMessage is { IsSuccessStatusCode: true }
            ? await httpResponseMessage.Content.ReadFromJsonAsync<TResult>(this.jsonSerializerOptions, cancellationToken)
            : default;
    }
}