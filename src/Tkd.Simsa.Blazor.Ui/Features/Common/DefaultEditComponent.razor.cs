namespace Tkd.Simsa.Blazor.Ui.Features.Common;

using MediatR;

using Microsoft.AspNetCore.Components;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Blazor.Ui.App;
using Tkd.Simsa.Domain.Common;

public partial class DefaultEditComponent<TItem, TEditItem>
    where TItem : IModelWithId<Guid>
    where TEditItem : IEditItem<TItem, TEditItem>
{
    [Parameter]
    public required RenderFragment<TEditItem> FormContent { get; set; }

    [Parameter]
    public Guid? ItemId { get; set; }

    [Parameter]
    public required ItemType ItemType { get; set; }

    [CascadingParameter]
    public ThemeProvider ThemeProvider { get; set; } = default!;

    private TEditItem EditItem { get; set; } = TEditItem.New();

    private string Error { get; set; } = string.Empty;

    private bool IsError => !string.IsNullOrEmpty(this.Error);

    private bool IsNew { get; set; }

    [Inject]
    private IMediator Mediator { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        if (this.ItemId is { } itemId)
        {
            var itemById = await this.Mediator.Send(new GetItemByIdQuery<TItem>(itemId));

            if (itemById is not null)
            {
                this.EditItem = TEditItem.FromModel(itemById);
            }
            else
            {
                this.Error = $"{this.ItemType.Name.Singular} with id {itemId} not found.";
            }
        }

        this.IsNew = this.ItemId is null;

        await base.OnInitializedAsync();
    }

    private async Task SaveAsync(TEditItem item)
    {
        if (this.IsNew)
        {
            await this.Mediator.Send(new AddItemCommand<TItem>(item.ToModel()));
        }
        else
        {
            await this.Mediator.Send(new UpdateItemCommand<TItem>(item.ToModel()));
        }

        this.NavigationManager.NavigateTo($"{this.ItemType.Route}");
    }
}