namespace Tkd.Simsa.Blazor.Ui.App;

using Microsoft.AspNetCore.Components;

using MudBlazor;

public partial class ThemeProvider : ComponentBase
{
    public readonly Variant Variant = Variant.Filled;

    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}