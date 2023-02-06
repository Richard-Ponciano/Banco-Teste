using Banco.Domain.Contract.Model;

namespace Banco.Domain.Contract
{
    public class ContaHistoricoModel
        : EFBaseModel<int>
    {
        public ContaHistoricoModel(int contaId, decimal credito, decimal debito, decimal saldo, int? criadoPor)
        {
            ContaId = contaId > 0 ? contaId : throw new Exception("* Conta Inválida.");

            if(credito == 0 && debito == 0 && saldo == 0)
                throw new Exception("* Valores Inválidos.");

            Credito = credito;
            Debito = debito;
            Saldo = saldo;
            CriadoPor = (criadoPor ?? 0) > 0 ? criadoPor : throw new Exception("* Sem Usuário para o Log");
        }

        public int ContaId { get; private set; } // int
        public decimal Credito { get; private set; } // decimal(10,2)
        public decimal Debito { get; private set; } // decimal(10,2)
        public decimal Saldo { get; private set; } // decimal(12,2)
        public DateTime CriadoEm { get; private set; } = DateTime.Now; // datetime
        public int? CriadoPor { get; private set; } = null;// int

        public ContaModel Conta { get; private set; }
    }
}