using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Banco.Domain.Cliente.Command;
using Banco.Domain.Contract;
using Banco.Domain.Contract.ViewModel;
using Banco.Infra.Data.Contract;
using Banco.Service.Contract;
using MediatR;

namespace Banco.Service
{
    public class ClienteService
        : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;

        public ClienteService(
            IClienteRepository repository,
            IMapper mapper,
            IMediator mediatR)
        {
            _repository = repository;
            _mapper = mapper;
            _mediatR = mediatR;
        }

        public Task<int> AddAsync(ClienteVM view)
        {
            if (view == null)
                throw new ArgumentNullException("* Cliente vazio");

            try
            {
                var model = new ClienteModel(view.LoginId, view.Documento, view.PessoaFisica, view.Nome, view.Sobrenome, 1); // Alterar CriadoPor
                return _mediatR.Send(new AddClienteCommand(model));
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<ClienteVM[]> GetAllAsync()
        {
            var lst = await _repository.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? _mapper.Map<ClienteVM[]>(lst)
                : null;
        }

        public Task<ClienteVM> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdAsync(ClienteVM view)
        {
            throw new NotImplementedException();
        }
    }
}