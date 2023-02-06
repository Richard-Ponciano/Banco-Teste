using Banco.Domain.Contract;

namespace Banco.Domain.Login.Command
{
    public class UpdLoginCommand
        : LoginCommand<bool>
    {
        public UpdLoginCommand(
            LoginModel entity)
            : base(entity)
        { }
    }
}