using Banco.Domain.Contract;
using MediatR;

namespace Banco.Domain.Cliente.Command
{
    public abstract class ClienteCommand<TResult>
        : IRequest<TResult>
    {
        public readonly ClienteModel Entity;

        public ClienteCommand(
            ClienteModel entity)
        {
            Entity = entity;
        }
    }
}