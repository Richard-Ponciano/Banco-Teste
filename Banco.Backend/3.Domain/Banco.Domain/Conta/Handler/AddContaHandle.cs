using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Domain.Cliente.Command;
using Banco.Domain.Conta.Command;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.Conta.Handler
{
    public class AddContaHandle
        : ContaHandle, IRequestHandler<AddContaCommand, int>
    {
        public AddContaHandle(
            IContaRepository repository)
            : base(repository)
        { }

        public async Task<int> Handle(AddContaCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Cliente vazia.");

            var exists = await _repository.GetSingleAsync(c => 
                c.Agencia.Equals(request.Entity.Agencia) &&
                c.Numero.Equals(request.Entity.Numero)).ConfigureAwait(false);
            if (exists == null)
            {
                try
                {
                    return await _repository.AddAsync(request.Entity).ConfigureAwait(false);
                }
                catch(Exception)
                {
                    throw;
                }
            }
            return exists.Id;
        }
    }
}