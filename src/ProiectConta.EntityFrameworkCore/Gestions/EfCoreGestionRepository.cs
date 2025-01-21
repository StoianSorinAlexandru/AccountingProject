using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ProiectConta.Gestions
{
    public class EfCoreGestionRepository : EfCoreRepository<ProiectContaDbContext, Gestion, Guid>, IGestionRepository
    {
        public EfCoreGestionRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Gestion> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(gestion => gestion.Name == name);
        }

        public async Task<List<Gestion>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    gestion => gestion.Name.Contains(filter)
                )
                //.OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<Gestion> GetAsync(Guid id)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(gestion => gestion.Id == id);

        }



    }
}
