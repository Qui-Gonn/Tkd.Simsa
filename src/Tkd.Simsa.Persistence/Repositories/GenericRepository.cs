namespace Tkd.Simsa.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using Tkd.Simsa.Application.Common;
using Tkd.Simsa.Domain.Common;
using Tkd.Simsa.Persistence.Mapper;

internal abstract class GenericRepository<TEntity, TModel> : IGenericRepository<TModel>
    where TEntity : class, IHasId<Guid>
    where TModel : class, IHasId<Guid>
{
    public GenericRepository(SimsaDbContext dbContext, IMapper<TEntity, TModel> mapper)
    {
        this.DbContext = dbContext;
        this.Mapper = mapper;
    }

    protected DbSet<TEntity> Data => this.DbContext.Set<TEntity>();

    protected SimsaDbContext DbContext { get; }

    protected IMapper<TEntity, TModel> Mapper { get; }

    protected TransactionMode TransactionMode { get; set; } = TransactionMode.Auto;

    public async ValueTask<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var entity = this.Mapper.ToEntity(model);
        var entityEntry = this.Data.Add(entity);

        if (this.TransactionMode.IsAuto)
        {
            await this.SaveChangesAsync(cancellationToken);
            return await this.GetByIdAsync(entity.Id, cancellationToken);
        }

        return this.Mapper.ToModel(entityEntry.Entity);
    }

    public async ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingEntity = await this.GetEntityByIdAsync(id, cancellationToken);
        this.Data.Remove(existingEntity);

        if (this.TransactionMode.IsAuto)
        {
            await this.SaveChangesAsync(cancellationToken);
        }
    }

    public async ValueTask<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        => (await this.Data
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Select(this.Mapper.ToModel);

    public async ValueTask<TModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => this.Mapper.ToModel(await this.GetEntityByIdAsync(id, cancellationToken));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => this.DbContext.SaveChangesAsync(cancellationToken);

    public void SetTransactionMode(TransactionMode transactionMode) => this.TransactionMode = transactionMode;

    public async ValueTask<TModel> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
    {
        var existingEntity = await this.GetEntityByIdAsync(model.Id, cancellationToken);

        this.Mapper.UpdateEntity(existingEntity, model);
        var entityEntry = this.Data.Update(existingEntity);

        if (this.TransactionMode.IsAuto)
        {
            await this.SaveChangesAsync(cancellationToken);
            return await this.GetByIdAsync(model.Id, cancellationToken);
        }

        return this.Mapper.ToModel(entityEntry.Entity);
    }

    protected async ValueTask<TEntity> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.Data.FindAsync([id], cancellationToken)
           ?? throw new KeyNotFoundException();
}