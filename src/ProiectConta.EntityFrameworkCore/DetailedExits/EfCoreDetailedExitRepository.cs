using ProiectConta.EntityFrameworkCore;
using System;
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
    }
}
