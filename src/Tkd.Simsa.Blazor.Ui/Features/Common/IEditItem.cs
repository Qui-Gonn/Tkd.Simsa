namespace Tkd.Simsa.Blazor.Ui.Features.Common;

using Tkd.Simsa.Domain.Common;

public interface IEditItem<TItem, out TEditItem> : IEditItem<TItem>
    where TItem : IModelWithId<Guid>
    where TEditItem : IEditItem<TItem>
{
    static abstract TEditItem FromModel(TItem source);

    static abstract TEditItem New();

    TItem ToModel();
}

public interface IEditItem<TItem>
    where TItem : IModelWithId<Guid>
{
    public TItem Source { get; init; }
}