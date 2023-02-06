using Banco.Domain.Contract.ViewModel;

namespace Banco.Service.Contract
{
    public interface ILoginService
        : IServiceBase<LoginVM, int>
    {
        Task<string> GetAcessTokenAsync(LoginAcessVM vm);
    }
}