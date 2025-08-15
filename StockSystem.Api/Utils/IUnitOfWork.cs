using StockSystem.Api.Domain;
using StockSystem.Api.Repository;

namespace StockSystem.Api.Utils
{
    public interface IUnitOfWork
    {
        int Save();
        IRepository<TEntity> Repo<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveAsync();
    }
}