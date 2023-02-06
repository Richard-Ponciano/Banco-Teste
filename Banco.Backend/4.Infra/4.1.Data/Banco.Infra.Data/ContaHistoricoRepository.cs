using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Domain.Contract;
using Banco.Infra.Data.Contract;

namespace Banco.Infra.Data
{
    public class ContaHistoricoRepository
        : RepositoryBase<ContaHistoricoModel, int>, IContaHistoricoRepository
    {
        public ContaHistoricoRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
