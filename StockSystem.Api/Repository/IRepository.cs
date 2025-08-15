using StockSystem.Api.Domain;
using StockSystem.Api.Utils;

namespace StockSystem.Api.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IPagedList<TEntity> GetAllPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false);

        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Table { get; }

    }
}