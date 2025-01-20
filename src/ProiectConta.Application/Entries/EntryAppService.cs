using Polly.Simmy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Entries
{
    public class EntryAppService : ApplicationService, IEntryAppService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly EntryManager _entryManager;

        public EntryAppService(IEntryRepository entryRepository, EntryManager entryManager)
        {
            _entryRepository = entryRepository;
            _entryManager = entryManager;
        }

        public async Task<EntryDto> GetAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        public async Task<EntryDto> CreateAsync(CreateUpdateEntryDto input)
        {
            var entry = await _entryManager.CreateAsync(
                input.Date,
                input.PartnerId,
                input.GestionId
            );
            await _entryRepository.InsertAsync(entry, autoSave: true);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateEntryDto input)
        {
            var entry = await _entryRepository.GetAsync(id);
            if (entry.Date != input.Date)
            {
                await _entryManager.ChangeDateAsync(entry, input.Date);
            }
            entry.PartnerId = input.PartnerId;
            entry.GestionId = input.GestionId;
            await _entryRepository.UpdateAsync(entry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _entryRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<EntryDto>> GetListAsync(GetEntryListDto input)
        {
            var entries = await _entryRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = await _entryRepository.GetCountAsync();
            return new PagedResultDto<EntryDto>(
                totalCount,
                ObjectMapper.Map<List<Entry>, List<EntryDto>>(entries)
            );
        }
    }
}
