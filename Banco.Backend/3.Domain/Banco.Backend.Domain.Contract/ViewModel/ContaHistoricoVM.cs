namespace Banco.Domain.Contract.ViewModel
{
    public class ContaHistoricoVM
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Credito { get; set; }
        public decimal Debito { get; set; }
        public decimal Saldo { get; set; }
    }
}