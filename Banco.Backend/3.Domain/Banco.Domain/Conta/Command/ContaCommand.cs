using Banco.Domain.Contract;
using MediatR;

namespace Banco.Domain.Conta.Command
{
    public abstract class ContaCommand<TResult>
        : IRequest<TResult>
    {
        public readonly ContaModel Entity;

        public ContaCommand(
            ContaModel entity)
        {
            Entity = entity;
        }
    }
}