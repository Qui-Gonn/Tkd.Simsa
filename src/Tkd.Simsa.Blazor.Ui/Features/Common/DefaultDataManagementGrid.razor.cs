namespace Tkd.Simsa.Blazor.Ui.Features.Common;

using System.Linq.Expressions;

using MediatR;

using Microsoft.AspNetCore.Components;

using MudBlazor;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Domain.Common;

public partial class DefaultDataManagementGrid<TItem>
    where TItem : IModelWithId<Guid>
{
    [Parameter]
    public required RenderFragment Columns { get; set; }

    [Parameter]
    public required Expression<Func<TItem, string>> ItemTextExpression { get; set; }

    [Parameter]
    public required ItemType ItemType { get; set; }

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private IEnumerable<TItem> Items { get; set; } = [];

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await this.ReloadDataAsync();
        await base.OnInitializedAsync();
    }

    private void Add()
        => this.NavigationManager.NavigateTo($"{this.ItemType.Route}/add");

    private async Task Delete(TItem item)
    {
        var confirmed = await this.DialogService.ShowMessageBox(
            $"Delete {this.GetItemText(item)}",
            new MarkupString(
                $"""Are you sure you want to delete <strong class="mud-secondary-text">{this.GetItemText(item)}</strong>? This action can not be undone!"""),
            "Yes",
            "No");

        if (confirmed ?? false)
        {
            await this.Mediator.Send(new DeleteItemCommand<TItem>(item.Id));
            await this.ReloadDataAsync();
        }
    }

    private void Edit(TItem item)
        => this.NavigationManager.NavigateTo($"{this.ItemType.Route}/edit/{item.Id}");

    private string GetItemText(TItem item)
        => this.ItemTextExpression.Compile().Invoke(item);

    private async Task ReloadDataAsync()
        => this.Items = await this.Mediator.Send(new GetItemsQuery<TItem>(QueryParams.Empty<TItem>()));
}