namespace Tkd.Simsa.Domain.Common;

public interface IModelWithId<out TId> : IModel, IHasId<TId>;