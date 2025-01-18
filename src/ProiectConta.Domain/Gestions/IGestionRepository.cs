using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Gestions
{
    public interface IGestionRepository : IRepository<Gestion, Guid>
    {
    }
}
