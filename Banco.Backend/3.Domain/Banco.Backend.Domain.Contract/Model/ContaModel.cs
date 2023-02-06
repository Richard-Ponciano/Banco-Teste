using Banco.Domain.Contract.Model;

namespace Banco.Domain.Contract
{
    public class ContaModel
        : EFBaseModel<int>
    {
        public ContaModel(int clienteId, string agencia, string numero, bool ativa, int criadoPor)
        {
            ClienteId = clienteId > 0 ? clienteId : throw new Exception("* Cliente Inválido.");

            Agencia = agencia?.Trim() ?? throw new ArgumentNullException("* Agência Inválida.");
            Agencia = agencia.Length > 1 && agencia.Length <= 4
                ? agencia
                : throw new Exception("* Agência deve conter entre 1 e 4 caracteres.");

            Numero = numero?.Trim() ?? throw new ArgumentNullException("* Número da Conta Inválido.");
            Numero = numero.Length > 1 && numero.Length <= 10
                ? numero
                : throw new Exception("* Número da Conta deve conter entre 1 e 10 caracteres.");

            Ativa = ativa;
            CriadoPor = criadoPor > 0 ? criadoPor : throw new Exception("* Sem Usuário para o Log");
        }

        public ContaModel(int id, int clienteId, string agencia, string numero, bool ativa, int criadoPor, DateTime criadoEm, int atualizadoPor)
            : this(clienteId, agencia, numero, ativa, criadoPor)
        {
            Id = id > 0 ? id : throw new Exception("* Conta Inválida.");
            CriadoEm = criadoEm;
            AtualizadoEm = DateTime.Now;
            AtualizadoPor = atualizadoPor > 0 ? atualizadoPor : throw new Exception("* Sem Usuário para o Log");
        }

        public int ClienteId { get; private set; }
        public string Agencia { get; private set; } // varchar(4)
        public string Numero { get; private set; } // varchar(10)
        public bool Ativa { get; private set; }
        public DateTime CriadoEm { get; private set; } = DateTime.Now; // datetime
        public int CriadoPor { get; private set; } // int
        public DateTime? AtualizadoEm { get; private set; } = null; // datetime
        public int? AtualizadoPor { get; private set; } = null; // int

        public ClienteModel Cliente { get; private set; } = null;
        public IEnumerable<ContaHistoricoModel> ContaHistoricoes { get; private set; } = null;
    }
}