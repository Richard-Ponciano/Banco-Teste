using Banco.Domain.ContaHistorico.Command;
using Banco.Domain.Contract;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.ContaHistorico.Handler
{
    public class UpdContaHistoricoHandle
        : ContaHistoricoHandle, IRequestHandler<UpdContaHistoricoCommand, bool>
    {
        public UpdContaHistoricoHandle(
            IContaHistoricoRepository repository)
            : base(repository)
        { }

        public async Task<bool> Handle(UpdContaHistoricoCommand request, CancellationToken cancellationToken)
        {
            var lstSaldo = await _repository.GetSingleAsync(
                c => c.ContaId.Equals(request.Entity.ContaId),
                c => c.CriadoEm,
                false).ConfigureAwait(false);

            try
            {
                var saldo = (request.Entity.Credito != 0)
                    ? ((lstSaldo?.Saldo ?? 0) + request.Entity.Credito)
                    : ((lstSaldo?.Saldo ?? 0) - request.Entity.Debito);

                var model = new ContaHistoricoModel(request.Entity.ContaId, request.Entity.Credito, request.Entity.Debito, saldo, 1);

                return (await _repository.AddAsync(model).ConfigureAwait(false)) > 0;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}