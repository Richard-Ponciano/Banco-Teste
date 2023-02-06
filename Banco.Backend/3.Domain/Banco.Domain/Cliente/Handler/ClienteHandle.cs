using Banco.Infra.Data.Contract;

namespace Banco.Domain.Cliente.Handler
{
    public abstract class ClienteHandle
    {
        internal readonly IClienteRepository _repository;

        protected ClienteHandle(
            IClienteRepository repository)
        {
            _repository = repository;
        }
    }
}