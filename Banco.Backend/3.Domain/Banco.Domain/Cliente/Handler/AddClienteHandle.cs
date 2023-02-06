using Banco.Domain.Cliente.Command;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.Cliente.Handler
{
    public class AddClienteHandle
        : ClienteHandle, IRequestHandler<AddClienteCommand, int>
    {
        public AddClienteHandle(
            IClienteRepository repository)
            : base(repository)
        { }

        public async Task<int> Handle(AddClienteCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Cliente vazia.");

            var exists = await _repository.GetSingleAsync(c => c.Documento.Equals(request.Entity.Documento)).ConfigureAwait(false);
            if(exists == null)
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