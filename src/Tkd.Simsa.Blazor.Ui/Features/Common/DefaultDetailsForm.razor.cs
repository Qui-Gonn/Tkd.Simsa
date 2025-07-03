namespace Tkd.Simsa.Blazor.Ui.Features.Common;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor;

public partial class DefaultDetailsForm<TEditItem>
{
    private MudForm? formRef;

    [Parameter]
    public required TEditItem EditItem { get; set; }

    [Parameter]
    public required RenderFragment<TEditItem> FormContent { get; set; }

    [Parameter]
    public required ItemType ItemType { get; set; }

    [Parameter]
    public EventCallback<TEditItem> OnSave { get; set; }

    private bool IsValid { get; set; }

    [Inject]
    private IJSRuntime JsRuntime { get; set; } = null!;

    private async Task CancelAsync()
    {
        await this.JsRuntime.InvokeVoidAsync("history.back");
    }

    private async Task SaveAsync()
    {
        this.formRef?.Validate();
        if (!this.IsValid)
        {
            return;
        }

        if (this.OnSave.HasDelegate)
        {
            await this.OnSave.InvokeAsync(this.EditItem);
        }
    }
}