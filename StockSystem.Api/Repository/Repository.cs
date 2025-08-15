using Microsoft.EntityFrameworkCore;
using StockSystem.Api.Data;
using StockSystem.Api.Domain;
using StockSystem.Api.Extensions;
using StockSystem.Api.Utils;

namespace StockSystem.Api.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext dbContext;
        private DbSet<TEntity> dbSet;
        public IQueryable<TEntity> Table => dbSet;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {

            if (entity is ISoftDelete s)
            {
                s.DeletedOnUtc = DateTime.UtcNow;
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public IPagedList<TEntity> GetAllPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = func != null ? func(Table) : Table;

            return query.ToPagedList(pageIndex, pageSize, getOnlyTotalCount);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }
        public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
    }
}