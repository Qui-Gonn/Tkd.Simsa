namespace Tkd.Simsa.Persistence.Test.Helper;

using Tkd.Simsa.Domain.Common;

internal class TestEntity(Guid id, string value) : IHasId<Guid>
{
    public Guid Id { get; set; } = id;

    public string Value { get; set; } = value;
}