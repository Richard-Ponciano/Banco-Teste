using Banco.Domain.Contract;

namespace Banco.Domain.Cliente.Command
{
    public class UpdClienteCommand
        : ClienteCommand<bool>
    {
        public UpdClienteCommand(
            ClienteModel entity)
            : base(entity)
        { }
    }
}