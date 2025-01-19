using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ProiectConta.Partners
{
    public class PartnerAppService : ApplicationService, IPartnerAppService
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly PartnerManager _partnerManager;

        public PartnerAppService(IPartnerRepository partnerRepository, PartnerManager partnerManager)
        {
            _partnerRepository = partnerRepository;
            _partnerManager = partnerManager;
        }

        public async Task<PartnerDto> GetAsync(Guid id)
        {
            var partner = await _partnerRepository.GetAsync(id);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task<PartnerDto> GetPartnerAsync(string name)
        {
            var partner = await _partnerRepository.FindByNameAsync(name);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task<PartnerDto> CreateAsync(CreateUpdatePartnerDto input)
        {
            var partner = await _partnerManager.CreateAsync(
                input.Name,
                input.CUI,
                input.Address,
                input.Type
            );
            await _partnerRepository.InsertAsync(partner, autoSave: true);
            return ObjectMapper.Map<Partner, PartnerDto>(partner);
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePartnerDto input)
        {
            var partner = await _partnerRepository.GetAsync(id);
            if (partner.Name != input.Name)
            {
                await _partnerManager.ChangeNameAsync(partner, input.Name);
            }
            partner.CUI = input.CUI;
            partner.Address = input.Address;
            partner.Type = input.Type;
            await _partnerRepository.UpdateAsync(partner);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _partnerRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<PartnerDto>> GetListAsync(GetPartnerListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Partner.Name);
            }
            var partners = await _partnerRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = await _partnerRepository.GetCountAsync();

            return new PagedResultDto<PartnerDto>(
                totalCount,
                ObjectMapper.Map<List<Partner>, List<PartnerDto>>(partners)
            );
        }

    }
}
