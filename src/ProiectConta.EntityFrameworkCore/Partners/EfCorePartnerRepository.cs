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

        public async Task<List<Partner>> GetPartnersByTypeAsync(PartnerType type)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Partners
                .Where(p => p.Type == type)
                .ToListAsync();
        }
    }
}
