using Banco.Domain.Contract;

namespace Banco.Domain.ContaHistorico.Command
{
    public class UpdContaHistoricoCommand
        : ContaHistoricoCommand<bool>
    {
        public UpdContaHistoricoCommand(
            ContaHistoricoModel entity)
            : base(entity)
        { }
    }
}