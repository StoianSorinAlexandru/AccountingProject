using Volo.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.DetailedEntries
{
    public interface IDetailedEntryRepository : IRepository<DetailedEntry, Guid>
    {
        Task<DetailedEntry> FindByEntryIdAsync(Guid id);

        Task<List<DetailedEntry>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );
    }
}
