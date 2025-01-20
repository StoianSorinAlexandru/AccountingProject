using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Entries
{
    public interface IEntryRepository : IRepository<Entry, Guid>
    {
        Task<Entry> FindByDateAsync(DateTime date);

        Task<List<Entry>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            );
    }
}
