using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Exits
{
    public interface IExitRepository : IRepository<Exit, Guid>
    {
        Task<Exit> FindByDateAsync(DateTime date);

        Task<List<Exit>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );

    }
}
