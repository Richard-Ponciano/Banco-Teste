using AutoMapper;
using Banco.Domain.Conta.Command;
using Banco.Domain.ContaHistorico.Command;
using Banco.Domain.Contract;
using Banco.Domain.Contract.ViewModel;
using Banco.Infra.Data.Contract;
using Banco.Service.Contract;
using MediatR;

namespace Banco.Service
{
    public class ContaService
        : IContaService
    {
        private readonly IContaRepository _repository;
        private readonly IMediator _mediatoR;
        private readonly IMapper _mapper;

        public ContaService(
            IContaRepository repository,
            IMediator mediatoR,
            IMapper mapper)
        {
            _repository = repository;
            _mediatoR = mediatoR;
            _mapper = mapper;
        }

        public Task<int> AddAsync(ContaVM view)
        {
            if(view == null)
                throw new ArgumentNullException("* Conta vazia");

            try
            {
                var model = new ContaModel(view.ClienteId, view.Agencia, view.NumConta, true, 1);
                return _mediatoR.Send(new AddContaCommand(model));
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ContaVM[]> GetAllAsync()
        {
            var lst = await _repository.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? _mapper.Map<ContaVM[]>(lst)
                : null;
        }

        public Task<ContaVM> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdAsync(ContaVM view)
        {
            throw new NotImplementedException();
        }

        public async Task<bool?> AtualizarSaldoContaAsync(UpdSaldoContaVM vm)
        {
            var contaModel = await _repository.GetSingleAsync(c => c.Agencia == vm.Agencia && c.Numero == vm.NumConta).ConfigureAwait(false);
            if (contaModel != null)
            {
                var model = vm.Credito
                    ? new UpdContaHistoricoCommand(new ContaHistoricoModel(contaModel.Id, vm.Valor, 0, 0, 1))
                    : new UpdContaHistoricoCommand(new ContaHistoricoModel(contaModel.Id, 0, vm.Valor, 0, 1));

                return await _mediatoR.Send(model).ConfigureAwait(false);
            }
            return null;
        }
    }
}