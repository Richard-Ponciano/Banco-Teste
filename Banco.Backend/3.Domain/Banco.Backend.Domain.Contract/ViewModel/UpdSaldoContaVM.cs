namespace Banco.Domain.Contract.ViewModel
{
    public class UpdSaldoContaVM
    {
        public string Agencia { get; set; }
        public string NumConta { get; set; }
        public decimal Valor { get; set; }
        public bool Credito { get; set; }
    }
}