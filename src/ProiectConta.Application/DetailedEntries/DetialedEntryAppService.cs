using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace ProiectConta.DetailedEntries
{
    public class DetailedEntryAppService : ApplicationService, IDetailedEntryAppService
    {
        private readonly IDetailedEntryRepository _detailedEntryRepository;

        public DetailedEntryAppService(IDetailedEntryRepository detailedEntryRepository)
        {
            _detailedEntryRepository = detailedEntryRepository;
        }

        public async Task<DetailedEntryDto> GetAsync(Guid id)
        {
            var detailedEntry = await _detailedEntryRepository.GetAsync(id);
            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(detailedEntry);
        }

        public async Task<List<DetailedEntryDto>> GetListAsync()
        {
            var detailedEntries = await _detailedEntryRepository.GetListAsync();
            return ObjectMapper.Map<List<DetailedEntry>, List<DetailedEntryDto>>(detailedEntries);
        }

        public async Task<DetailedEntryDto> CreateAsync(CreateUpdateDetailedEntryDto input)
        {
            var detailedEntry = ObjectMapper.Map<CreateUpdateDetailedEntryDto, DetailedEntry>(input);
            var createdDetailedEntry = await _detailedEntryRepository.InsertAsync(detailedEntry);
            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(createdDetailedEntry);
        }

        public async Task<DetailedEntryDto> UpdateAsync(Guid id, CreateUpdateDetailedEntryDto input)
        {
            var detailedEntry = await _detailedEntryRepository.GetAsync(id);
            ObjectMapper.Map(input, detailedEntry);
            var updatedDetailedEntry = await _detailedEntryRepository.UpdateAsync(detailedEntry);
            return ObjectMapper.Map<DetailedEntry, DetailedEntryDto>(updatedDetailedEntry);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _detailedEntryRepository.DeleteAsync(id);
        }
    }
}
