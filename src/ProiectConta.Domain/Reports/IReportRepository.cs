using Volo.Abp.Domain.Repositories;
using System;

namespace ProiectConta.Reports
{
    public interface IReportRepository : IRepository<Report, Guid>
    {
    }
}
