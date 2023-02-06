using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Banco.Domain.Contract;
using Banco.Domain.Contract.ViewModel;
using Banco.Domain.Login.Command;
using Banco.Infra.Data.Contract;
using Banco.Service.Contract;
using MediatR;

namespace Banco.Service
{
    public class LoginService
        : ILoginService
    {
        private readonly ILoginRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediatR;

        public LoginService(
            ILoginRepository repository,
            IMapper mapper,
            IMediator mediatR)
        {
            _repository = repository;
            _mapper = mapper;
            _mediatR = mediatR;
        }

        public Task<int> AddAsync(LoginVM view)
        {
            if(view == null)
                throw new ArgumentNullException("* Login vazio");

            try
            {
                var model = new LoginModel(view.Usuario, view.Senha, 1);
                return _mediatR.Send(new AddLoginCommand(model));
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<LoginVM[]> GetAllAsync()
        {
            var lst = await _repository.GetAllAsync().ConfigureAwait(false);
            return (lst?.Any() ?? false)
                ? _mapper.Map<LoginVM[]>(lst)
                : null;
        }

        public Task<LoginVM> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdAsync(LoginVM view)
        {
            if(view == null)
                throw new ArgumentNullException("* Login vazio");

            if(view.Id <= 0)
                throw new Exception("* Id inválido");

            var exists = await _repository.GetByIdAsync(view.Id).ConfigureAwait(false);
            if (exists != null)
            {
                var model = new LoginModel(view.Id, exists.Usuario, view.Senha, exists.CriadoPor, exists.CriadoEm, 1);

                return await _mediatR.Send(new UpdLoginCommand(model)).ConfigureAwait(false);
            }
            return false;
        }

        public Task<string> GetAcessTokenAsync(LoginAcessVM vm)
        {
            return _mediatR.Send(new LoginAcessCommand(vm));
        }
    }
}