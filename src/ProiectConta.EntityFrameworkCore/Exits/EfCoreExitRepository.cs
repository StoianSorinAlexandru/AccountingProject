using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProiectConta.Exits
{
    public class EfCoreExitRepository : EfCoreRepository<ProiectContaDbContext, Exit, Guid>, IExitRepository
    {
        public EfCoreExitRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Exit> FindByDateAsync(DateTime date)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(exit => exit.Date == date);
        }

        public async Task<List<Exit>> GetListAsync(
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
