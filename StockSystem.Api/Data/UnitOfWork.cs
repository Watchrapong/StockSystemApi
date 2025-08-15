using StockSystem.Api.Domain;
using StockSystem.Api.Repository;
using StockSystem.Api.Utils;

namespace StockSystem.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IRepository<TEntity> Repo<TEntity>() where TEntity : BaseEntity
        {
            return new Repository<TEntity>(appDbContext);
        }

        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return appDbContext.SaveChangesAsync();
        }
    }
}