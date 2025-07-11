namespace Tkd.Simsa.Persistence.Mapper;

using Tkd.Simsa.Application.Common.Filtering;

internal interface IMapper<TEntity, TModel>
{
    IPropertyMapper<TEntity, TModel> PropertyMapper { get; }

    TEntity ToEntity(TModel model);

    TModel ToModel(TEntity entity);

    TEntity UpdateEntity(TEntity entity, TModel model);
}