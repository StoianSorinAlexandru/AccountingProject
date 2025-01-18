using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

namespace ProiectConta.Entries
{
    public class EfCoreEntryRepository : EfCoreRepository<ProiectContaDbContext, Entry, Guid>, IEntryRepository
    {
        public EfCoreEntryRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
