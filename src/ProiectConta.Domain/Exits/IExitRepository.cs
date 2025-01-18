using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Exits
{
    public interface IExitRepository : IRepository<Exit, Guid>
    {
    }
}
