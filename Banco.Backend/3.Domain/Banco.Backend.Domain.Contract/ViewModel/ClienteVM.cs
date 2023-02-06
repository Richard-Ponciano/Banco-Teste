namespace Banco.Domain.Contract.ViewModel
{
    public class ClienteVM
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string Documento { get; set; }
        public bool PessoaFisica { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}