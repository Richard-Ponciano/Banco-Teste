namespace Banco.Domain.Contract.ViewModel
{
    public class ContaVM
    {
        public int ClienteId { get; set; }
        public string Agencia { get; set; }
        public string NumConta { get; set; }
        public bool Ativa { get; set; }
    }
}