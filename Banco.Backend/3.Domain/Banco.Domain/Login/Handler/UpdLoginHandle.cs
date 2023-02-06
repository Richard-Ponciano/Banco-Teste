using Banco.Domain.Login.Command;
using Banco.Infra.Data.Contract;
using MediatR;

namespace Banco.Domain.Login.Handler
{
    public class UpdLoginHandle
        : LoginHandle, IRequestHandler<UpdLoginCommand, bool>
    {
        public UpdLoginHandle(
            ILoginRepository repository)
            : base(repository)
        { }

        public Task<bool> Handle(UpdLoginCommand request, CancellationToken cancellationToken)
        {
            if(request.Entity == null)
                throw new Exception("* Entidade Login vazia.");

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