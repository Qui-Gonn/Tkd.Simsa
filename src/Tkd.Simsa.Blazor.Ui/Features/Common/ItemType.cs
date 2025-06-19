namespace Tkd.Simsa.Blazor.Ui.Features.Common;

using Tkd.Simsa.Blazor.Ui.App;

public record ItemType(ItemTypeName Name, string Route);

public record ItemTypeName(string Singular, string Plural);

public static class ItemTypeNames
{
    public static readonly ItemTypeName Event = new ("Event", "Events");

    public static readonly ItemTypeName Person = new ("Person", "Persons");
}

public static class ItemTypes
{
    public static readonly ItemType Event = new (ItemTypeNames.Event, RouteConstants.EventManagement);

    public static readonly ItemType Person = new (ItemTypeNames.Person, RouteConstants.PersonManagement);
}