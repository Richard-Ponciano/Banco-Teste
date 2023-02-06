using Banco.Domain.Contract.ViewModel;

namespace Banco.Service.Contract
{
    public interface IContaService
        : IServiceBase<ContaVM, int>
    {
        Task<bool?> AtualizarSaldoContaAsync(UpdSaldoContaVM vm);
    }
}