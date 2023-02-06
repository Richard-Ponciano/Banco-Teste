using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco.Domain.Contract;
using Banco.Infra.Data.Contract;

namespace Banco.Infra.Data
{
    public class ContaRepository
        : RepositoryBase<ContaModel, int>, IContaRepository
    {
        public ContaRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
