using Volo.Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProiectConta.DetailedEntries
{
    public interface IDetailedEntryRepository : IRepository<DetailedEntry, Guid>
    {
        // Add custom methods if necessary
    }
}
