namespace Tkd.Simsa.Application.Common.Filtering;

public record FilterOperator(string OperatorName);

public static class FilterOperators
{
    public static readonly FilterOperator Contains = new (nameof(Contains));

    public static readonly FilterOperator EqualTo = new (nameof(EqualTo));

    public static readonly FilterOperator StartsWith = new (nameof(StartsWith));
}