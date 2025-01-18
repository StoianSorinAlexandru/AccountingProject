using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.Entries
{
    public class EntryAppService : ApplicationService, IEntryAppService
    {
        private readonly IEntryRepository _entryRepository;

        public EntryAppService(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<EntryDto> GetAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        public async Task<List<EntryDto>> GetListAsync()
        {
            var entries = await _entryRepository.GetListAsync();
            return ObjectMapper.Map<List<Entry>, List<EntryDto>>(entries);
        }

        public async Task<EntryDto> CreateAsync(CreateUpdateEntryDto input)
        {
            var entry = ObjectMapper.Map<CreateUpdateEntryDto, Entry>(input);
            var createdEntry = await _entryRepository.InsertAsync(entry);
            return ObjectMapper.Map<Entry, EntryDto>(createdEntry);
        }

        public async Task<EntryDto> UpdateAsync(Guid id, CreateUpdateEntryDto input)
        {
            var entry = await _entryRepository.GetAsync(id);
            ObjectMapper.Map(input, entry);
            var updatedEntry = await _entryRepository.UpdateAsync(entry);
            return ObjectMapper.Map<Entry, EntryDto>(updatedEntry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _entryRepository.DeleteAsync(id);
        }
    }
}
