using ACDB.DynamicSql.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ACDB.DynamicSql.DAL.Repositories
{
    public class BaseRepository<TId, TEntity, TContext> : IBaseRepository<TId, TEntity>
        where TEntity : class, new()
        where TContext : DbContext
    {
        public BaseRepository(TContext context)
        {
            DbContext = context;
        }

        protected TContext DbContext { get; }

        public void Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsync(TId id, CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().FindAsync(id, ct);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken ct = default)
        {
            return await DbContext.Set<TEntity>().ToListAsync(ct);
        }

        public void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Update(entity);
        }
    }
}