using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace ProiectConta.Entries
{
    public class EntryManager : DomainService
    {
        private readonly IEntryRepository _entryRepository;
        public EntryManager(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }
        public async Task<Entry> CreateAsync(
            DateTime date,
            Guid partnerId,
            Guid gestionId)
        {
            return new Entry(
                GuidGenerator.Create(),
                date,
                partnerId,
                gestionId
            );
        }

        public async Task ChangeDateAsync(Entry entry, DateTime date)
        {
            entry.Date = date;
        }
    }
}
