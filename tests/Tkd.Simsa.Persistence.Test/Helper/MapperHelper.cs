namespace Tkd.Simsa.Persistence.Test.Helper;

using System.Linq.Expressions;

using Tkd.Simsa.Application.Common.Filtering;
using Tkd.Simsa.Persistence.Mapper;

internal static class MapperHelper
{
    public static IMapper<TestEntity, TestModel> TestMapper { get; } = new TestMapperImplementation();

    public static IPropertyMapper<TestEntity, TestModel> TestPropertyMapper => TestMapper.PropertyMapper;

    private class TestMapperImplementation : IMapper<TestEntity, TestModel>, IPropertyMapper<TestEntity, TestModel>
    {
        public IPropertyMapper<TestEntity, TestModel> PropertyMapper => this;

        public TestEntity ToEntity(TestModel model) => new (model.Id, model.Value);

        public Expression<Func<TestEntity, object>> ToEntityPropertyExpression(string propertyName)
            => propertyName switch
            {
                nameof(TestModel.Id) => entity => entity.Id,
                nameof(TestModel.Value) => entity => entity.Value,
                _ => throw new NotSupportedException(propertyName)
            };

        public TestModel ToModel(TestEntity entity) => new (entity.Id, entity.Value);

        public TestEntity UpdateEntity(TestEntity entity, TestModel model)
        {
            entity.Value = model.Value;
            return entity;
        }
    }
}