namespace Tkd.Simsa.Application.Common.Filtering;

public sealed record PagingParameters(int PageNumber, int PageSize)
{
    public static readonly PagingParameters NoPaging = new (0, 0);
}