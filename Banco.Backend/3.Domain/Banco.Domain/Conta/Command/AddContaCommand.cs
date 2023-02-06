using Banco.Domain.Contract;

namespace Banco.Domain.Conta.Command
{
    public class AddContaCommand
        : ContaCommand<int>
    {
        public AddContaCommand(
            ContaModel entity)
            : base(entity)
        { }
    }
}