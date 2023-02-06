using Banco.Domain.Contract;

namespace Banco.Domain.Login.Command
{
    public class AddLoginCommand
        : LoginCommand<int>
    {
        public AddLoginCommand(
            LoginModel entity)
            : base(entity)
        { }
    }
}