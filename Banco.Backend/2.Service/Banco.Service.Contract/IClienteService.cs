using Banco.Domain.Contract.ViewModel;

namespace Banco.Service.Contract
{
    public interface IClienteService
        : IServiceBase<ClienteVM, int>
    { }
}