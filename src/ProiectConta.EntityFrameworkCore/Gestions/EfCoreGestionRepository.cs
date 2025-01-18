using Volo.Abp.EntityFrameworkCore;
using System;
using ProiectConta.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;

namespace ProiectConta.Gestions
{
    public class EfCoreGestionRepository : EfCoreRepository<ProiectContaDbContext, Gestion, Guid>, IGestionRepository
    {
        public EfCoreGestionRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
