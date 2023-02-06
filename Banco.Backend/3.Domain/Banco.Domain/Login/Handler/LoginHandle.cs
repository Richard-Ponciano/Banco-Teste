using Banco.Infra.Data.Contract;

namespace Banco.Domain.Login.Handler
{
    public abstract class LoginHandle
    {
        internal readonly ILoginRepository _repository;

        protected LoginHandle(
            ILoginRepository repository)
        {
            _repository = repository;
        }
    }
}