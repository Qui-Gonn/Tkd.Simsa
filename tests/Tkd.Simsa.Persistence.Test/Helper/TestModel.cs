namespace Tkd.Simsa.Persistence.Test.Helper;

using Tkd.Simsa.Domain.Common;

internal record TestModel(Guid Id, string Value) : IHasId<Guid>;