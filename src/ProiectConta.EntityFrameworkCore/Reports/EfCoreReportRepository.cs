using ProiectConta.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ProiectConta.Reports
{
    public class EfCoreReportRepository : EfCoreRepository<ProiectContaDbContext, Report, Guid>, IReportRepository
    {
        public EfCoreReportRepository(IDbContextProvider<ProiectContaDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
