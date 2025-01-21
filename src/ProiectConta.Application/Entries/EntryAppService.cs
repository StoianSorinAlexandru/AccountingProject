using Polly.Simmy;
using ProiectConta.DetailedEntries;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Entries
{
    public class EntryAppService : ApplicationService, IEntryAppService
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IPartnerRepository _partnerRepository;
        private readonly IGestionRepository _gestionRepository;
        private readonly IDetailedEntryRepository _detailedEntryRepository;
        private readonly EntryManager _entryManager;

        public EntryAppService(
            IEntryRepository entryRepository, 
            IPartnerRepository partnerRepository, 
            IGestionRepository gestionRepository, 
            IDetailedEntryRepository detailedEntryRepository, 
            EntryManager entryManager)
        {
            _entryRepository = entryRepository;
            _partnerRepository = partnerRepository;
            _gestionRepository = gestionRepository;
            _detailedEntryRepository = detailedEntryRepository;
            _entryManager = entryManager;
        }

        public async Task<EntryDto> GetAsync(Guid id)
        {
            var entry = await _entryRepository.GetAsync(id);
            return ObjectMapper.Map<Entry, EntryDto>(entry);
        }

        public async Task<EntryDto> CreateAsync(CreateUpdateEntryDto input)
        {

            var partner = await _partnerRepository.FindByNameAsync(input.PartnerName);
            var gestion = await _gestionRepository.FindByNameAsync(input.GestionName);

            var entry = await _entryManager.CreateAsync(
                input.Date,
                partner.Id,
                gestion.Id
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

            var partner = await _partnerRepository.FindByNameAsync(input.PartnerName);
            var gestion = await _gestionRepository.FindByNameAsync(input.GestionName);
            entry.PartnerId = partner.Id;
            entry.GestionId = gestion.Id;
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

            var entryListDto = new List<EntryDto>();
            foreach (var entry in entries)
            {
                var partnerName = await _partnerRepository.GetAsync(entry.PartnerId);
                var gestionName = await _gestionRepository.GetAsync(entry.GestionId);
                entryListDto.Add(new EntryDto
                {
                    Id = entry.Id,
                    Date = entry.Date,
                    PartnerName = partnerName.Name,
                    GestionName = gestionName.Name
                });
            }

            return new PagedResultDto<EntryDto>(
                totalCount,
                entryListDto.AsReadOnly()
            );
        }
    }
}
