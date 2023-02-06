using Banco.Domain.Contract.Model;

namespace Banco.Domain.Contract
{
    public class LoginModel
        : EFBaseModel<int>
    {
        public LoginModel(string usuario, string senha, int criadoPor)
        {
            Usuario = usuario?.Trim() ?? throw new ArgumentNullException("* Usuário Inválido.");
            Usuario = usuario.Length >= 3 && usuario.Length <= 50
                ? usuario
                : throw new Exception("* Usuário deve conter entre 3 e 50 caracteres.");

            Senha = senha.Length >= 3 && senha.Length <= 50
                ? senha
                : throw new Exception("* Senha deve conter entre 3 e 50 caracteres.");

            CriadoPor = criadoPor > 0 ? criadoPor : throw new Exception("* Sem Usuário para o Log.");
        }

        public LoginModel(int id, string usuario, string senha, int criadoPor, DateTime criadoEm, int atualizadoPor) : this(usuario, senha, criadoPor)
        {
            Id = id > 0 ? id : throw new Exception("* Login Inválido.");
            CriadoEm = criadoEm;
            AtualizadoPor = atualizadoPor > 0 ? atualizadoPor : throw new Exception("* Sem Usuário para o Log.");
            AtualizadoEm = DateTime.Now;
        }

        public string Usuario { get; private set; } // varchar(50)
        public string Senha { get; private set; } // varchar(50)
        public DateTime CriadoEm { get; private set; } = DateTime.Now; // datetime
        public int CriadoPor { get; private set; } // int
        public DateTime? AtualizadoEm { get; private set; } = null; // datetime
        public int? AtualizadoPor { get; private set; } = null; // int

        public ClienteModel Cliente { get; private set; } = null;
    }
}