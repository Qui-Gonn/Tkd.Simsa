namespace Tkd.Simsa.DataGenerator;

using Bogus;

using Microsoft.Extensions.DependencyInjection;

using Tkd.Simsa.Application.Common;

public class DataGenerator(IServiceProvider serviceProvider)
{
    private const int Count = 25;

    private readonly FakerCollection fakerCollection = new ();

    public async Task PopulateDatabaseAsync(CancellationToken cancellationToken = default)
    {
        await this.PopulateAsync(this.fakerCollection.EventFaker, cancellationToken);
        await this.PopulateAsync(this.fakerCollection.PersonFaker, cancellationToken);
    }

    private async Task PopulateAsync<TModel>(Faker<TModel> faker, CancellationToken cancellationToken = default)
        where TModel : class
    {
        faker.AssertConfigurationIsValid();
        var generatedItems = faker.Generate(Count);

        var repository = serviceProvider.GetRequiredService<IGenericRepository<TModel>>();
        repository.SetTransactionMode(TransactionMode.Manual);

        foreach (var item in generatedItems)
        {
            await repository.AddAsync(item, cancellationToken);
        }

        await repository.SaveChangesAsync(cancellationToken);
    }
}