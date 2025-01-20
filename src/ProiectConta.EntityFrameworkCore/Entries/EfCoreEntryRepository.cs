using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ProiectConta.Entries
{
    public class EfCoreEntryRepository : EfCoreRepository<ProiectContaDbContext, Entry, Guid>, IEntryRepository
    {
        public EfCoreEntryRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Entry> FindByDateAsync(DateTime date)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(entry => entry.Date == date);
        }

        public async Task<List<Entry>> GetListAsync(
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
