using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

namespace ProiectConta.Exits
{
    public class EfCoreExitRepository : EfCoreRepository<ProiectContaDbContext, Exit, Guid>, IExitRepository
    {
        public EfCoreExitRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
