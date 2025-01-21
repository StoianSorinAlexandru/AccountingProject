using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.DetailedEntries
{
    
    public class DetailedEntryManager : DomainService
    {
        private readonly IDetailedEntryRepository _detailedEntryRepository;

        public DetailedEntryManager(IDetailedEntryRepository detailedEntryRepository)
        {
            _detailedEntryRepository = detailedEntryRepository;
        }

        public async Task<DetailedEntry> CreateAsync(
            Guid entryId,
            Guid productId,
            int quantity,
            float value)
        {
            return new DetailedEntry(
                GuidGenerator.Create(),
                entryId,
                productId,
                quantity,
                value
            );
        }
    }
}
