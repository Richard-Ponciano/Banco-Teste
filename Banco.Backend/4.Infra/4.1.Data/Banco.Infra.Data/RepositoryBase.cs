using System.Linq.Expressions;
using Banco.Domain.Contract.Model;
using Banco.Infra.Data.Contract;
using Microsoft.EntityFrameworkCore;

namespace Banco.Infra.Data
{
    public class RepositoryBase<TEntity, TId>
        : IRepositoryBase<TEntity, TId>
        where TEntity : EFBaseModel<TId>
        where TId : struct
    {
        internal readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(
            ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException($"Sem Contexto para: {nameof(context)}");
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TId> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entity.Id;
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> where) =>
            (await GetSingleAsync(where).ConfigureAwait(false)) != null;

        public virtual Task<TEntity[]> GetAllAsync(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> orderBy,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var consulta = _dbSet.AsNoTracking().AsQueryable<TEntity>().Where(where);
            if(includes != null)
            {
                consulta = includes.Aggregate(consulta, (current, include) => current.Include(include));
            }

            return consulta.OrderBy(orderBy).ToArrayAsync();
        }

        public virtual Task<TEntity[]> GetAllAsync() =>
            _dbSet.AsNoTracking().ToArrayAsync();

        public virtual Task<TEntity> GetByIdAsync(TId id) => GetSingleAsync(e => e.Id.Equals(id));

        public virtual Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> orderBy,
            bool orderByAsc,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var consulta = _dbSet.AsQueryable<TEntity>().Where(where);
            if(includes != null)
            {
                consulta = includes.Aggregate(consulta, (current, include) => current.Include(include));
            }

            consulta = orderByAsc
                ? consulta.OrderBy(orderBy)
                : consulta.OrderByDescending(orderBy);

            return consulta.FirstOrDefaultAsync();
        }

        public virtual Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes) => GetSingleAsync(where, (x => x.Id), true, includes);

        public virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> where) => GetSingleAsync(where, (x => x.Id), true, null);

        public async Task<bool> UpdAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}