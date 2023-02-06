using Banco.Domain.Contract;
using MediatR;

namespace Banco.Domain.ContaHistorico.Command
{
    public abstract class ContaHistoricoCommand<TResult>
        : IRequest<TResult>
    {
        public readonly ContaHistoricoModel Entity;

        protected ContaHistoricoCommand(
            ContaHistoricoModel entity)
        {
            Entity = entity;
        }
    }
}