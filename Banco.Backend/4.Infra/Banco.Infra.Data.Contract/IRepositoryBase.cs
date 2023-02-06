using System.Linq.Expressions;
using Banco.Domain.Contract.Model;

namespace Banco.Infra.Data.Contract
{
    public interface IRepositoryBase<TEntity, TId>
        where TEntity : EFBaseModel<TId>
        where TId : struct
    {
        Task<TId> AddAsync(TEntity entity);
        Task<bool> Exists(Expression<Func<TEntity, bool>> where);
        Task<TEntity[]> GetAllAsync(
            Expression<Func<TEntity, bool>> where, 
            Expression<Func<TEntity, object>> orderBy,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity[]> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, object>> orderBy,
            bool orderByAsc,
            params Expression<Func<TEntity, object>>[] includes);
        Task<bool> UpdAsync(TEntity entity);
    }
}