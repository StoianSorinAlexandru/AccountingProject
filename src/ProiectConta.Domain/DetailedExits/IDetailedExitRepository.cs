using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.DetailedExits
{
    public interface IDetailedExitRepository : IRepository<DetailedExit, Guid>
    {
        Task<DetailedExit> FindByExitIdAsync(Guid id);

        Task<List<DetailedExit>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );

    }
}
