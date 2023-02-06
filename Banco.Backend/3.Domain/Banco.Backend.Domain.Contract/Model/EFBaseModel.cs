namespace Banco.Domain.Contract.Model
{
    public abstract class EFBaseModel<TId>
        where TId : struct
    {
        public TId Id { get; set; }
    }
}