using Banco.Domain.Contract;
using MediatR;

namespace Banco.Domain.Login.Command
{
    public abstract class LoginCommand<TResult>
        : IRequest<TResult>
    {
        public readonly LoginModel Entity;

        public LoginCommand(
            LoginModel entity)
        {
            Entity = entity;
        }
    }
}