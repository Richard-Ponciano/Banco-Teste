using Banco.Domain.Contract;

namespace Banco.Domain.Cliente.Command
{
    public class AddClienteCommand
        : ClienteCommand<int>
    {
        public AddClienteCommand(
            ClienteModel entity)
            : base(entity)
        { }
    }
}