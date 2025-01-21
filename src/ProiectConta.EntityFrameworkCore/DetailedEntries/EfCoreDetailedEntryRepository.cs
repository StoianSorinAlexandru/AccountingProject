using Microsoft.EntityFrameworkCore;
using ProiectConta.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;

namespace ProiectConta.DetailedEntries
{
    public class EfCoreDetailedEntryRepository : EfCoreRepository<ProiectContaDbContext, DetailedEntry, Guid>, IDetailedEntryRepository
    {
        public EfCoreDetailedEntryRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DetailedEntry> FindByEntryIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(
                de => de.EntryId == id
                );
        }

        public async Task<List<DetailedEntry>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
            )
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
