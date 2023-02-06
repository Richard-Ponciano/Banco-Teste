using Banco.Domain.Contract.ViewModel;
using MediatR;

namespace Banco.Domain.Login.Command
{
    public class LoginAcessCommand
        : IRequest<string>
    {
        public readonly LoginAcessVM Entity;

        public LoginAcessCommand(
            LoginAcessVM entity)
        {
            Entity = entity;
        }
    }
}