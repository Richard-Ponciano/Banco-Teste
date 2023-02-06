using Banco.Domain.Login.Command;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.Login.Handler
{
    public class AddLoginHandle
        : LoginHandle, IRequestHandler<AddLoginCommand, int>
    {
        public AddLoginHandle(
            ILoginRepository repository)
            : base(repository)
        { }

        public async Task<int> Handle(AddLoginCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Login vazia.");

            var exists = await _repository.GetSingleAsync(l => l.Usuario.Equals(request.Entity.Usuario)).ConfigureAwait(false);
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