using System.Text.RegularExpressions;
using Banco.Domain.Contract.Model;

namespace Banco.Domain.Contract
{
    public class ClienteModel
        : EFBaseModel<int>
    {
        public ClienteModel(int loginId, string documento, bool pessoaFisica, string nome, string sobrenome, int criadoPor)
        {
            LoginId = loginId > 0 ? loginId : throw new Exception("* Sem Login para o Cliente.");

            Documento = documento?.Trim() ?? throw new ArgumentNullException("* Documento Inválido.");
            Documento = !string.IsNullOrEmpty(documento)
                ? Regex.Replace(documento, @"\D|\s", string.Empty)
                : throw new ArgumentNullException("* Documento Inválido.");
            
            PessoaFisica = pessoaFisica;

            Nome = nome?.Trim() ?? throw new ArgumentNullException("* Nome Inválido.");
            Nome = nome.Length >= 2 && nome.Length <= 50
                ? nome
                : throw new Exception("* Nome deve conter entre 2 e 50 caracteres.");

            Sobrenome = sobrenome?.Trim() ?? throw new ArgumentNullException("* Sobrenome Inválido");
            Sobrenome = sobrenome.Length >= 2 && nome.Length <= 100
                ? sobrenome
                : throw new Exception("* Sobrenome deve conter entre 2 e 100 caracteres.");

            CriadoPor = criadoPor > 0 ? criadoPor : throw new Exception("* Sem Usuário para o Log");
        }

        public ClienteModel(int id, int loginId, string documento, bool pessoaFisica, string nome, string sobrenome, int criadoPor, DateTime criadoEm, int atualizadoPor) : this(loginId, documento, pessoaFisica, nome, sobrenome, criadoPor)
        {
            Id = id > 0 ? id : throw new Exception("* Cliente Inválido");
            CriadoEm = criadoEm;
            AtualizadoEm = DateTime.Now;
            AtualizadoPor = atualizadoPor > 0 ? atualizadoPor : throw new Exception("* Sem Usuário para o Log");
        }

        public int LoginId { get; private set; }
        public string Documento { get; private set; } // varchar(14)
        public bool PessoaFisica { get; private set; } // bit
        public string Nome { get; private set; } // varchar(50)
        public string Sobrenome { get; private set; } // varchar(100)
        public DateTime CriadoEm { get; private set; } = DateTime.Now; // datetime
        public int CriadoPor { get; private set; } // int
        public DateTime? AtualizadoEm { get; private set; } = null; // datetime
        public int? AtualizadoPor { get; private set; } = null; // int

        public ContaModel Conta { get; private set; } = null;
        public LoginModel Login { get; private set; } = null;
    }
}