using Banco.Infra.Data.Contract;

namespace Banco.Domain.Conta.Handler
{
    public abstract class ContaHandle
    {
        internal readonly IContaRepository _repository;

        protected ContaHandle(
            IContaRepository repository)
        {
            _repository = repository;
        }
    }
}