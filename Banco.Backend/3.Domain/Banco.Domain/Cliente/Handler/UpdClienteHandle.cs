using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Domain.Cliente.Command;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.Cliente.Handler
{
    public class UpdClienteHandle
        : ClienteHandle, IRequestHandler<UpdClienteCommand, bool>
    {
        public UpdClienteHandle(
            IClienteRepository repository) 
            : base(repository)
        { }

        public Task<bool> Handle(UpdClienteCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Cliente vazia.");

            try
            {
                return _repository.UpdAsync(request.Entity);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}