using ProiectConta.DetailedEntries;
using ProiectConta.Gestions;
using ProiectConta.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Exits
{
    public class ExitAppService : ApplicationService, IExitAppService
    {
        private readonly IExitRepository _exitRepository; 
        private readonly IPartnerRepository _partnerRepository;
        private readonly IGestionRepository _gestionRepository;
        private readonly IDetailedEntryRepository _detailedEntryRepository;
        private readonly ExitManager _exitManager;

        public ExitAppService(
            IExitRepository exitRepository,
            IPartnerRepository partnerRepository,
            IGestionRepository gestionRepository,
            IDetailedEntryRepository detailedEntryRepository,
            ExitManager exitManager)
        {
            _exitRepository = exitRepository;
            _partnerRepository = partnerRepository;
            _gestionRepository = gestionRepository;
            _detailedEntryRepository = detailedEntryRepository;
            _exitManager = exitManager;
        }

        public async Task<ExitDto> GetAsync(Guid id)
        {
            var exit = await _exitRepository.GetAsync(id);
            return ObjectMapper.Map<Exit, ExitDto>(exit);
        }

        public async Task<ExitDto> CreateAsync(CreateUpdateExitDto input)
        {
            var exit = await _exitManager.CreateUnrelatedAsync(
                input.Date
            );

            var partner = await _partnerRepository.FindByNameAsync(input.PartnerName);
            var gestion = await _gestionRepository.FindByNameAsync(input.GestionName);

            exit.PartnerId = partner.Id;
            exit.GestionId = gestion.Id;
            await _exitRepository.InsertAsync(exit);

            return ObjectMapper.Map<Exit, ExitDto>(exit);
        }

        public async Task UpdateAsync(Guid id, CreateUpdateExitDto input)
        {
            var exit = await _exitRepository.GetAsync(id);
            if (exit.Date != input.Date)
            {
                await _exitManager.ChangeDateAsync(exit, input.Date);
            }
            var partner = await _partnerRepository.FindByNameAsync(input.PartnerName);
            var gestion = await _gestionRepository.FindByNameAsync(input.GestionName);
            exit.PartnerId = partner.Id;
            exit.GestionId = gestion.Id;
            await _exitRepository.UpdateAsync(exit);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _exitRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<ExitDto>> GetListAsync(GetExitListDto input)
        {
            var exits = await _exitRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );
            var totalCount = await _exitRepository.GetCountAsync();

            var exitDtoList = new List<ExitDto>();
            foreach (var exit in exits)
            {
                var partner = await _partnerRepository.GetAsync(exit.PartnerId);
                var gestion = await _gestionRepository.GetAsync(exit.GestionId);
                exitDtoList.Add(new ExitDto
                {
                    Id = exit.Id,
                    Date = exit.Date,
                    PartnerName = partner.Name,
                    GestionName = gestion.Name
                });
            }

            return new PagedResultDto<ExitDto>(
                totalCount,
                exitDtoList.AsReadOnly()
            );
        }


    }
}
