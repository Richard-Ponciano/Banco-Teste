using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Domain.Contract;

namespace Banco.Infra.Data.Contract
{
    public interface IContaHistoricoRepository
        : IRepositoryBase<ContaHistoricoModel, int>
    {
    }
}
