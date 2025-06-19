namespace Tkd.Simsa.Domain.Common;

public interface IHasId<out TId>
{
    public TId Id { get; }
}