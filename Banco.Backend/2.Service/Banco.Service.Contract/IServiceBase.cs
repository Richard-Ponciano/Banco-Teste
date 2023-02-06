namespace Banco.Service.Contract
{
    public interface IServiceBase<TView, TId>
        where TId : struct
        where TView : class
    {
        Task<TId> AddAsync(TView view);
        Task<bool> UpdAsync(TView view);
        Task<TView> GetByIdAsync(TId id);
        Task<TView[]> GetAllAsync();
    }
}