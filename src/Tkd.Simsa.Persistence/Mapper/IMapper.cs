namespace Tkd.Simsa.Persistence.Mapper;

using Tkd.Simsa.Persistence.Filtering;

internal interface IMapper<TEntity, TModel> : IPropertyMapper<TEntity, TModel>
{
    TEntity ToEntity(TModel model);

    TModel ToModel(TEntity entity);

    TEntity UpdateEntity(TEntity entity, TModel model);
}