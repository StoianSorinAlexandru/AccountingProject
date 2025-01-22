using Microsoft.EntityFrameworkCore;
using ProiectConta.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProiectConta.DetailedExits
{
    public class EfCoreDetailedExitRepository : EfCoreRepository<ProiectContaDbContext, DetailedExit, Guid>, IDetailedExitRepository
    {
        public EfCoreDetailedExitRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DetailedExit> FindByExitIdAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(
                de => de.ExitId == id
                );
        }

        public async Task<List<DetailedExit>> GetListAsync(
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
