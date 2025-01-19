using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using System.Linq;

namespace ProiectConta.Partners
{
    public class EfCorePartnerRepository : EfCoreRepository<ProiectContaDbContext, Partner, Guid>, IPartnerRepository
    {
        public EfCorePartnerRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Partner> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(partner => partner.Name == name);
        }

        public async Task<List<Partner>> GetPartnersByTypeAsync(PartnerType type)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(partner => partner.Type == type).ToListAsync();
        }

        public async Task<List<Partner>> GetListAsync(
            int skipCount, 
            int maxResultCount, 
            string sorting, 
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    partner => partner.Name.Contains(filter)
                )
                //.OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }


    }
}
