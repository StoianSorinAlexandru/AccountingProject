using ProiectConta.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProiectConta.DetailedEntries
{
    public class EfCoreDetailedEntryRepository : EfCoreRepository<ProiectContaDbContext, DetailedEntry, Guid>, IDetailedEntryRepository
    {
        public EfCoreDetailedEntryRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
