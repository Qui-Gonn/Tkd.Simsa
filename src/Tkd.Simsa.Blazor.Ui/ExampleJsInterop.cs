namespace Tkd.Simsa.Blazor.Ui;

using Microsoft.JSInterop;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class ExampleJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public ExampleJsInterop(IJSRuntime jsRuntime)
    {
        this.moduleTask = new Lazy<Task<IJSObjectReference>>(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                                                                 "import",
                                                                 "./_content/Tkd.Simsa.Blazor.Ui/exampleJsInterop.js").AsTask());
    }

    public async ValueTask DisposeAsync()
    {
        if (this.moduleTask.IsValueCreated)
        {
            var module = await this.moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    public async ValueTask<string> Prompt(string message)
    {
        var module = await this.moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }
}