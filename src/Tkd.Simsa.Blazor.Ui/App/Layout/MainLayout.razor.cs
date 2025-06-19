namespace Tkd.Simsa.Blazor.Ui.App.Layout;

using MudBlazor;

public partial class MainLayout
{
    private readonly MudTheme theme = new ();

    private bool drawerOpen = true;

    private void DrawerToggle()
        => this.drawerOpen = !this.drawerOpen;
}