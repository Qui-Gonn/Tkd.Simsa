namespace Tkd.Simsa.Persistence.Test.Helper;

using NSubstitute;

using Tkd.Simsa.Persistence.Mapper;

internal static class MapperHelper
{
    public static IMapper<TestEntity, TestModel> SubstituteForTestEntityAndTestModel()
    {
        var mapperSubstitute = Substitute.For<IMapper<TestEntity, TestModel>>();

        mapperSubstitute.ToEntity(Arg.Any<TestModel>())
            .Returns(x => new TestEntity(x.Arg<TestModel>().Id, x.Arg<TestModel>().Value));

        mapperSubstitute.ToModel(Arg.Any<TestEntity>())
            .Returns(x => new TestModel(x.Arg<TestEntity>().Id, x.Arg<TestEntity>().Value));

        mapperSubstitute.UpdateEntity(Arg.Any<TestEntity>(), Arg.Any<TestModel>())
            .Returns(x =>
                     {
                         var testEntity = x.Arg<TestEntity>();
                         testEntity.Value = x.Arg<TestModel>().Value;
                         return testEntity;
                     });

        return mapperSubstitute;
    }
}