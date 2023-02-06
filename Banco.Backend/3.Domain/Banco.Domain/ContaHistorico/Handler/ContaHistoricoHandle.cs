using Banco.Infra.Data.Contract;

namespace Banco.Domain.ContaHistorico.Handler
{
    public abstract class ContaHistoricoHandle
    {
        internal readonly IContaHistoricoRepository _repository;

        protected ContaHistoricoHandle(
            IContaHistoricoRepository repository)
        {
            _repository = repository;
        }
    }
}