namespace Tkd.Simsa.Domain.PersonManagement;

public record PersonName(string FirstName, string LastName)
{
    private const string FormatLastNameFirstName = "{0}, {1}";

    public static readonly PersonName Empty = new (string.Empty, string.Empty);

    private string DisplayName { get; } = string.Format(FormatLastNameFirstName, LastName, FirstName);

    public override string ToString() => this.DisplayName;
}