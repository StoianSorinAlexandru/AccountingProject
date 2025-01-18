using Volo.Abp.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProiectConta.Entries
{
    public interface IEntryRepository : IRepository<Entry, Guid>
    {
        // Add custom methods if necessary
    }
}
